namespace DKCommunicationNET.Interface;

internal interface ISource/*:IACSource,IDCSource*/
{
    public IACSource ACSource { get; set; }
    public IDCSource DCSource { get; set; }

   

}