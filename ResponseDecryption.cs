using Fiddler;
using System;
using Standard;
using System.Windows.Forms;
using Util;

namespace Response
{
    public sealed class ResponseDecryption : Inspector2, IResponseInspector2, IBaseInspector2
    {
        private bool mBDirty;
        private bool mBReadOnly;
        private byte[] mBody;
        private HTTPResponseHeaders mResponseHeaders;
        private ResponseTextViewer mResponseTextViewer;

        public ResponseDecryption()
        {
            mResponseTextViewer = new ResponseTextViewer();
        }

        public bool bDirty
        {
            get
            {
                return mBDirty;
            }
        }

        public byte[] body
        {
            get
            {
                return mBody;
            }

            set
            {
                mBody = value;
                byte[] decodedBody = DoDecryption();
                if (decodedBody != null)
                {
                    mResponseTextViewer.body = decodedBody;
                }
                else
                {
                    mResponseTextViewer.body = value;
                }
            }
        }

        public byte[] DoDecryption()
        {
            // 将 byte[] 转成字符串
            String rawBody = System.Text.Encoding.Default.GetString(mBody);
            String showBody = rawBody;
            if (!rawBody.Contains("{"))
            {
                // 需要解密
                FiddlerApplication.Log.LogString("rawBody: " + rawBody);
                showBody = DecryptionUtil.DecryptSDKBody(rawBody);
            }
            if (showBody != null)
            {
                byte[] decodeBody = System.Text.Encoding.UTF8.GetBytes(showBody);
                return decodeBody;
            }
            else
            {
                Clear();
                return null;
            }
        }

        public bool bReadOnly
        {
            get
            {
                return mBReadOnly;
            }

            set
            {
                mBReadOnly = value;
            }
        }

        HTTPResponseHeaders IResponseInspector2.headers
        {
            get
            {
                FiddlerApplication.Log.LogString("headers get function.");
                return mResponseHeaders;
            }
            set
            {
                FiddlerApplication.Log.LogString("headers set function.");
                mResponseHeaders = value;
            }
        }

        public override void AddToTab(TabPage o)
        {
            mResponseTextViewer.AddToTab(o);
            o.Text = "Decryption";
        }

        public void Clear()
        {
            mBody = null;
            mResponseTextViewer.Clear();
        }

        // 在 Tab 上的摆放位置
        public override int GetOrder() => 100;
    }
}
