namespace dotless.Core
{
	using System.Web;
	using System.Web.SessionState;
	using System.Configuration;
	using System.Web.Configuration;
	using System;
	public class LessCssWithSessionHttpHandler : LessCssHttpHandler, IRequiresSessionState
	{
	}

	public class LessCssHttpHandler : LessCssHttpHandlerBase, IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			CustomErrorsSection customErrorSec = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
			var customErrorLevel = customErrorSec.Mode;

			try
			{
				var handler = Container.GetInstance<HandlerImpl>();

				handler.Execute();
			}
			catch (System.IO.FileNotFoundException ex)
			{
				context.Response.StatusCode = 404;
				if (customErrorLevel == CustomErrorsMode.Off
					|| context.Request.IsLocal)
				{
					context.Response.Write("/* File Not Found while parsing: " + ex.Message + " */");
				}
				else
				{
					context.Response.Write("/* Error Occurred. Consult log or view on local machine. */");
				}
				context.Response.End();
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				if (customErrorLevel == CustomErrorsMode.Off
					|| context.Request.IsLocal)
				{
					context.Response.Write("/* Error in less parsing: " + ex.Message + " */");
				}
				else
				{
					context.Response.Write("/* Error Occurred. Consult log or view on local machine. */");
				}
				context.Response.End();
			}
		}

		public bool IsReusable
		{
			get { return true; }
		}
	}
}
