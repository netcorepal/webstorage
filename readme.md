# NetCorePal.WebStorage

Web site storage component

Support ASP.NET 4.0+

Support ASP.NET Core 2.0+


## NetCorePal.WebStorage.Abstractions

Common use NetCorePal.WebStorage.WebStorageProvider


## NetCorePal.WebStorage.Local

Local disk storage provider
```
Startup.cs

public class Startup
{

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLocalWebStorage();  //add this code
        //other code
    }
}


public class HomeController : Controller
{
    WebStorageProvider webStorageProvider;
    public HomeController(WebStorageProvider provider)
    {
        this.webStorageProvider = provider;
    }
    public IActionResult Index()
    {
        var file = this.webStorageProvider.GetFile("/f/abc.txt");
        return View();
    }
}

or  

public IActionResult Index([FromServices]WebStorageProvider provider)
{
    var file = provider.GetFile("/f/abc.txt");
    return View();
}

```

## NetCorePal.WebStorage.AliyunOSS

Support .NET Framework 4.0 +, .net standard 2.0

```
WebStorageProvider webStorageProvider = new AliyunOSSWebStorageProvider(endpoint, bucketName, accessKeyId, accessKeySecret);
```