using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;
using System. IO. Ports;
using System. Threading;
using DKCommunicationNET. Core;
using DKCommunicationNET. LogNet;

namespace DKCommunicationNET. BaseClass
{
    /// <summary>
    /// 所有串行通信类的基类，提供了一些基础的服务
    /// </summary>
    public class SerialBase
    {
        #region Constructor

        /// <summary>
        /// 实例化一个无参的构造方法
        /// </summary>
        public SerialBase ( )
        {
            _SerialPort = new SerialPort ( );
            hybirdLock = new SimpleHybirdLock ( );
            _SerialPort. ReadTimeout = 1000;
            _SerialPort. WriteTimeout = 1000;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 初始化串口信息，115200波特率，8位数据位，1位停止位，无奇偶校验
        /// </summary>
        /// <param name="portName">端口号信息，例如"COM3"</param>
        public void SerialPortInni ( string portName )
        {
            SerialPortInni ( portName , 115200 );
        }

        /// <summary>
        /// 初始化串口信息，波特率，8位数据位，1位停止位，无奇偶校验
        /// </summary>
        /// <param name="portName">端口号信息，例如"COM3"</param>
        /// <param name="baudRate">波特率</param>
        public void SerialPortInni ( string portName , int baudRate )
        {
            SerialPortInni ( portName , baudRate , 8 , StopBits. One , Parity. None );
        }

        /// <summary>
        /// 初始化串口信息，波特率，数据位，停止位，奇偶校验需要全部自己来指定
        /// </summary>
        /// <param name="portName">端口号信息，例如"COM3"</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        /// <param name="parity">奇偶校验</param>
        public void SerialPortInni ( string portName , int baudRate , int dataBits , StopBits stopBits , Parity parity )
        {
            if ( _SerialPort. IsOpen )
            {
                return;
            }
            _SerialPort. PortName = portName;    // 串口
            _SerialPort. BaudRate = baudRate;    // 波特率
            _SerialPort. DataBits = dataBits;    // 数据位
            _SerialPort. StopBits = stopBits;    // 停止位
            _SerialPort. Parity = parity;      // 奇偶校验
        }

        /// <summary>
        /// 根据自定义初始化方法进行初始化串口信息
        /// </summary>
        /// <param name="initi">初始化的委托方法</param>
        public void SerialPortInni ( Action<SerialPort> initi )
        {
            if ( _SerialPort. IsOpen )
            {
                return;
            }
            _SerialPort. PortName = "COM5";
            _SerialPort. BaudRate = 115200;
            _SerialPort. DataBits = 8;
            _SerialPort. StopBits = StopBits. One;
            _SerialPort. Parity = Parity. None;

            initi. Invoke ( _SerialPort );
        }

        /// <summary>
        /// 打开一个新的串行端口连接
        /// </summary>
        public OperateResult Open ( )
        {
            if ( !_SerialPort. IsOpen )
            {
                try
                {
                    _SerialPort. Open ( );
                    InitializationOnOpen ( );
                    return OperateResult. CreateSuccessResult ( $"成功地打开了{_SerialPort. PortName}。");
                }
                catch ( Exception ex)
                {
                    return new OperateResult ( ex.Message );
                }
            }
            else
            {
                return OperateResult. CreateSuccessResult ( $"串行端口{_SerialPort.PortName}已经处于打开状态。" );
            }
        }

        /// <summary>
        /// 获取一个值，指示串口是否处于打开状态
        /// </summary>
        /// <returns>是或否</returns>
        public bool IsOpen ( )
        {
            return _SerialPort. IsOpen;
        }

        /// <summary>
        /// 关闭端口连接
        /// </summary>
        public OperateResult Close ( )
        {
            if ( _SerialPort. IsOpen )
            {
                try
                {
                    ExtraOnClose ( );
                    _SerialPort. Close ( );
                    return OperateResult. CreateSuccessResult ( $"成功地关闭了{_SerialPort. PortName}。" );
                }
                catch ( Exception ex )
                {
                    return new OperateResult ( ex. Message );
                }
            }
            else
            {
                return OperateResult. CreateSuccessResult ( $"串行端口{_SerialPort. PortName}已经处于关闭状态。" );
            }
        }

        /// <summary>
        /// 读取串口的数据
        /// </summary>
        /// <param name="send">发送的原始字节数据</param>
        /// <param name="awaitData">是否必须要等待数据返回</param>
        /// <returns>带接收字节的结果对象</returns>
        public OperateResult<byte[ ]> ReadBase ( byte[ ] send , bool awaitData = true )
        {
            hybirdLock. Enter ( );

            if ( IsClearCacheBeforeRead ) ClearSerialCache ( );

            OperateResult sendResult = SPSend ( _SerialPort , send );
            if ( !sendResult. IsSuccess )
            {
                hybirdLock. Leave ( );
                return OperateResult. CreateFailedResult<byte[ ]> ( sendResult );
            }

            OperateResult<byte[ ]> receiveResult = SPReceived ( _SerialPort , awaitData );
            hybirdLock. Leave ( );

            return receiveResult;
        }

        /// <summary>
        /// 清除串口缓冲区的数据，并返回该数据，如果缓冲区没有数据，返回的字节数组长度为0
        /// </summary>
        /// <returns>是否操作成功的方法</returns>
        public OperateResult<byte[ ]> ClearSerialCache ( )
        {
            return SPReceived ( _SerialPort , false );
        }

        #endregion

        #region virtual Method

        /// <summary>
        /// 检查当前接收的字节数据是否正确的
        /// </summary>
        /// <param name="rBytes">输入字节</param>
        /// <returns>检查是否正确</returns>
        protected virtual bool CheckReceiveBytes ( byte[ ] rBytes )
        {
            return true;
        }

        #endregion

        #region Initialization And Extra

        /// <summary>
        /// 在打开端口时的初始化方法，按照协议的需求进行必要的重写
        /// </summary>
        /// <returns>是否初始化成功</returns>
        protected virtual OperateResult InitializationOnOpen ( )
        {
            return OperateResult. CreateSuccessResult ( );
        }

        /// <summary>
        /// 在将要和服务器进行断开的情况下额外的操作，需要根据对应协议进行重写
        /// </summary>
        /// <returns>当断开连接时额外的操作结果</returns>
        protected virtual OperateResult ExtraOnClose ( )
        {
            return OperateResult. CreateSuccessResult ( );
        }

        #endregion

        #region Private Method

        /// <summary>
        /// 发送数据到串口里去
        /// </summary>
        /// <param name="serialPort">串口对象</param>
        /// <param name="data">字节数据</param>
        /// <returns>是否发送成功</returns>
        protected virtual OperateResult SPSend ( SerialPort serialPort , byte[ ] data )
        {
            if ( data != null && data. Length > 0 )
            {
                try
                {
                    serialPort. Write ( data , 0 , data. Length );
                    return OperateResult. CreateSuccessResult ( );
                }
                catch ( Exception ex )
                {
                    return new OperateResult ( ex. Message );
                }
            }
            else
            {
                return OperateResult. CreateSuccessResult ( );
            }
        }

        /// <summary>
        /// 从串口接收一串数据信息，可以指定是否一定要接收到数据
        /// </summary>
        /// <param name="serialPort">串口对象</param>
        /// <param name="awaitData">是否必须要等待数据返回</param>
        /// <returns>结果数据对象</returns>
        protected virtual OperateResult<byte[ ]> SPReceived ( SerialPort serialPort , bool awaitData = true )
        {
            byte[ ] buffer = new byte[1024];
            MemoryStream ms = new MemoryStream ( );
            DateTime start = DateTime. Now;                                  // 开始时间，用于确认是否超时的信息
            while ( true )
            {
                Thread. Sleep ( sleepTime );
                try
                {
                    if ( serialPort. BytesToRead < 1 )
                    {
                        if ( ( DateTime. Now - start ). TotalMilliseconds > ReceiveTimeout )
                        {
                            ms. Dispose ( );
                            return new OperateResult<byte[ ]> ( $"Time out: {ReceiveTimeout}" );
                        }
                        else if ( ms. Length > 0 )
                        {
                            break;
                        }
                        else if ( awaitData )
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }

                    // 继续接收数据
                    int sp_receive = serialPort. Read ( buffer , 0 , buffer. Length );
                    ms. Write ( buffer , 0 , sp_receive );
                }
                catch ( Exception ex )
                {
                    ms. Dispose ( );
                    return new OperateResult<byte[ ]> ( ex. Message );
                }
            }

            // resetEvent.Set( );
            byte[ ] result = ms. ToArray ( );
            ms. Dispose ( );
            return OperateResult. CreateSuccessResult ( result );
        }

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString ( )
        {
            return "SerialBase";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 当前的日志情况
        /// </summary>
        public ILogNet? LogNet
        {
            get { return logNet; }
            set { logNet = value; }
        }

        /// <summary>
        /// 接收数据的超时时间，默认5000ms
        /// </summary>
        public int ReceiveTimeout
        {
            get { return receiveTimeout; }
            set { receiveTimeout = value; }
        }

        /// <summary>
        /// 连续串口缓冲数据检测的间隔时间，默认20ms
        /// </summary>
        public int SleepTime
        {
            get { return sleepTime; }
            set { if ( value > 0 ) sleepTime = value; }
        }

        /// <summary>
        /// 是否在发送数据前清空缓冲数据，默认是false
        /// </summary>
        public bool IsClearCacheBeforeRead
        {
            get { return isClearCacheBeforeRead; }
            set { isClearCacheBeforeRead = value; }
        }

        /// <summary>
        /// 写入串口数据超时时间,默认1000ms
        /// </summary>
        public int WriteTimeOut
        {
            get { return _SerialPort. WriteTimeout; }
            set { if ( value > 0 ) _SerialPort. WriteTimeout = value; }
        }

        /// <summary>
        /// 写入串口数据超时时间,默认1000ms
        /// </summary>
        public int ReadTimeOut
        {
            get { return _SerialPort. ReadTimeout; }
            set { if ( value > 0 ) _SerialPort. ReadTimeout = value; }
        }

        #endregion

        #region Private Member
        /// <summary>
        /// 串口对象
        /// </summary>
        protected readonly SerialPort _SerialPort;

        // 数据交互的锁
        private readonly SimpleHybirdLock hybirdLock;

        // 日志存储
        private ILogNet? logNet;

        // 接收数据的超时时间
        private int receiveTimeout = 1000;

        // 睡眠的时间
        private int sleepTime = 50;

        // 是否在发送前清除缓冲
        private bool isClearCacheBeforeRead = false;
        #endregion Private Member
    }
}
