using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace BzReader
{
    /// <summary>
    /// The delegate for the UrlRequested event
    /// </summary>
    /// <param name="sender">The object which received the web browser request</param>
    /// <param name="e">Url which was requested</param>
    public delegate void UrlRequestedHandler(object sender, UrlRequestedEventArgs e);

    /// <summary>
    /// The class which used to pass information about the requested url to web browser
    /// </summary>
    public class UrlRequestedEventArgs : EventArgs
    {
        /// <summary>
        /// The topic which was request by web browser
        /// </summary>
        private string topicName;
        /// <summary>
        /// The name of the index we're going to request from
        /// </summary>
        private string indexName;
        /// <summary>
        /// Whether this is a redirect response
        /// </summary>
        private bool redirect;
        /// <summary>
        /// The redirect target
        /// </summary>
        private string redirectTarget;
        /// <summary>
        /// The response string
        /// </summary>
        private string response;
        /// <summary>
        /// The port the web server is listening on
        /// </summary>
        private int serverPort;

        /// <summary>
        /// The requested topic name
        /// </summary>
        public string TopicName
        {
            get { return topicName; }
        }

        /// <summary>
        /// The name of the index that is to be searched
        /// </summary>
        public string IndexName
        {
            get { return indexName; }
        }

        /// <summary>
        /// Whether this is a redirect response
        /// </summary>
        public bool Redirect
        {
            get { return redirect; }
            set { redirect = value; }
        }

        /// <summary>
        /// Redirection target
        /// </summary>
        public string RedirectTarget
        {
            get { return redirectTarget; }
            set { redirectTarget = value; }
        }

        /// <summary>
        /// Response string
        /// </summary>
        public string Response
        {
            get { return response; }
            set { response = value; }
        }

        /// <summary>
        /// The URL that is/was used to fetch this document
        /// </summary>
        public string Url
        {
            get { return String.Format("http://localhost:{0}/?topic={1}&index={2}", serverPort, HttpUtility.UrlEncode(topicName), HttpUtility.UrlEncode(indexName)); }
        }

        /// <summary>
        /// The url to the redirected topic
        /// </summary>
        public string RedirectUrl
        {
            get { return String.Format("http://localhost:{0}/?topic={1}&index={2}", serverPort, HttpUtility.UrlEncode(redirectTarget), HttpUtility.UrlEncode(indexName)); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRequestedEventArgs"/> class.
        /// </summary>
        /// <param name="aUrl">A URL which was request by web browser.</param>
        public UrlRequestedEventArgs(string aUrl, int port)
            : base()
        {
            serverPort = port;

            NameValueCollection nvc = HttpUtility.ParseQueryString(aUrl.Substring(1));

            topicName = nvc["topic"];
            indexName = nvc["index"];

            if (String.IsNullOrEmpty(topicName))
            {
                topicName = HttpUtility.UrlDecode(aUrl.Substring(1));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRequestedEventArgs"/> class.
        /// </summary>
        /// <param name="page">The page we are to generate the URL for</param>
        /// <param name="port">The port our server is listening on</param>
        public UrlRequestedEventArgs(PageInfo page, int port)
            : base()
        {
            topicName = page.Name;
            indexName = page.Indexer.File;
            redirect = !String.IsNullOrEmpty(page.RedirectToTopic);
            redirectTarget = page.RedirectToTopic;

            serverPort = port;
        }
    };
}
