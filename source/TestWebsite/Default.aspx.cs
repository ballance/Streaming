using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Born2Code.Net;
using System.Net.Sockets;

namespace TestWebsite
{
    public partial class _Default : System.Web.UI.Page
    {
        private const int BufferSize = 8192;

        /// <summary>
        /// Handles the Click event of the cmdDownload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            // Make sure the page is in valid state.
            if (!Page.IsValid)
            {
                return;
            }

            // Get the maximum bytes per second.
            int kbps = int.Parse(txtMaximumKbps.Text);
            long bps = kbps * 1024;

            string directory = Server.MapPath("~/");
            string filename = Path.Combine(directory, "10mb.bin");

            // Set Content information in header.
            FileInfo fileInfo = new FileInfo(filename);

            // If the file doesn't exist, throw 404.
            if (!fileInfo.Exists)
            {
                throw new HttpException(404, "File not found.");
            }

            // Set headers for response.
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
            Response.AppendHeader("Last-Modified", fileInfo.LastWriteTimeUtc.ToString("r"));
            Response.AppendHeader("Content-Type", "application/octet-stream");
            Response.AppendHeader("Content-Length", fileInfo.Length.ToString());

            // Openup source stream.
            using (FileStream sourceStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize))
            {
                // Create throttled destination stream.
                ThrottledStream destinationStream = new ThrottledStream(Response.OutputStream, bps);

                byte[] buffer = new byte[BufferSize];
                int readCount = sourceStream.Read(buffer, 0, BufferSize);

                Response.Buffer = false;

                while (readCount > 0)
                {
                    destinationStream.Write(buffer, 0, readCount);
                    readCount = sourceStream.Read(buffer, 0, BufferSize);
                }
            }
        }

        /// <summary>
        /// Handles the ServerValidate event of the cvMaximumKbps control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void cvMaximumKbps_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length == 0)
            {
                cvMaximumKbps.ErrorMessage = "This field is required.";
                args.IsValid = false;
            }
            else
            {
                int needed = 0;
                if (!Int32.TryParse(args.Value, out needed))
                {
                    cvMaximumKbps.ErrorMessage = "Not an valid value for an Int32.";
                    args.IsValid = false;
                }
            }
        }
    }
}
