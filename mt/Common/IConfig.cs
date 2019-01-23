namespace Mt.Common
{
    public interface IConfig
    {
       string Get(string keyName); 
       int GetInt(string keyName); 
    }
}