# UMC.DOME

用Visual Stuido 新建项目，选择ASP.NET应用程序（.Net Framework) ，再.Net Framework中选择4.6.1或更高，如下图：
![image](http://oss.365lu.cn/UserResources/1usm4ih/1585097891589/image.png)
确认后，添加UMC架构 UMC.Data.dll和实例UMC.Activities.dll，再引用对应的.数据库ADO.Net，如下图：
![image](http://oss.365lu.cn/UserResources/1usm4ih/1585099429382/image.png)

再新建一个WebContext类，代码如下:

```
namespace UMC.Demo
{
    public class WebContext : UMC.Net.NetContext
    {
        HttpContext _Context;
        public WebContext(HttpContext co)
        {
            this._Context = co;
        }
        public override void AddHeader(string name, string value)
        {
            this._Context.Response.AddHeader(name, value);
        }
        public override void AppendCookie(string name, string value)
        {
            _Context.Response.AppendCookie(new HttpCookie(name, value) { Expires = DateTime.Now.AddYears(100) });
        }

        public override NameValueCollection Cookies
        {
            get
            {
                if (_Cookies == null)
                {
                    _Cookies = new NameValueCollection();
                    for (var i = 0; i < _Context.Request.Cookies.Count; i++)
                    {
                        var c = _Context.Request.Cookies[i];
                        _Cookies.Add(c.Name, c.Value);

                    }

                }
                return _Cookies;
            }
        }
        NameValueCollection _Cookies;
        public override NameValueCollection Headers
        {
            get
            {
                return _Context.Request.Headers;
            }
        }
        public override NameValueCollection QueryString
        {
            get
            {
                return _Context.Request.QueryString;
            }
        }
        public override NameValueCollection Form
        {
            get
            {
                return this._Context.Request.Form;
            }
        }
        public override System.IO.Stream InputStream
        {
            get
            {
                if (_Context.Request.Files.Count > 0)
                {
                    return _Context.Request.Files[0].InputStream;
                }
                if (this.ContentType.IndexOf("www-form-urlencoded", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    return System.IO.Stream.Null;
                }

                return this._Context.Request.InputStream;
            }
        }
        public override System.IO.TextWriter Output
        {
            get
            {
                return this._Context.Response.Output;
            }
        }
        public override System.IO.Stream OutputStream
        {
            get
            {
                return this._Context.Response.OutputStream;
            }
        }
        public override string ContentType
        {
            get
            {
                return this._Context.Request.ContentType;
            }
            set
            {
                this._Context.Response.ContentType = value;
            }
        }
        public override string UserHostAddress
        {
            get { return this._Context.Request.UserHostAddress; }
        }
        public override string RawUrl
        {
            get { return this._Context.Request.RawUrl; }
        }
        public override string UserAgent
        {
            get { return this._Context.Request.UserAgent; }
        }
        public override Uri UrlReferrer
        {
            get { return this._Context.Request.UrlReferrer; }
        }
        public override Uri Url
        {
            get { return this._Context.Request.Url; }
        }
        public override void Redirect(string url)
        {
            this._Context.Response.Redirect(url, true);
        }
        public override int StatusCode
        {
            get
            {

                return this._Context.Response.StatusCode;
            }
            set
            {

                this._Context.Response.StatusCode = value;
            }
        }
        public override string HttpMethod
        {
            get { return this._Context.Request.HttpMethod; }
        }
    }
}
```
再新建一个类 WebHandler，代码如下：

```
namespace UMC.Demo
{
    public class WebHandler : System.Web.IHttpHandler
    {

        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {

            new Web.WebServlet().ProcessRequest(new WebContext(context));
        }

        #endregion
    }
}
```
再配置Web.Config的system.webServer/handlers节点如下：

```XML
<?xml version="1.0"?>
<configuration>
  <system.web>
    <compilation debug="true" targetFramework="4.6.1"/>
    <httpRuntime targetFramework="4.6"/>
    <customErrors mode="Off"/>
  </system.web>
  <system.webServer>
    <handlers>
      <add name="WebHandler" path="*" verb="*" type="UMC.Demo.WebHandler" preCondition="integratedMode,runtimeVersionv4.0"/>
    </handlers>
  </system.webServer>
</configuration>
```
再右击项目点击生成，这上标准的ASP.net项目，可以放在IIS上也可以直接运行，现在就可以用UMC开启你的全栈应用之旅了。
[UMC.Data.dll源码](https://github.com/wushunming/UMC.Data)




