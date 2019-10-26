using Fiddler;
using System;
using System.Windows.Forms;
using Standard;
using Util;

namespace Response
{
    public class ResponseDecryptionFormat : Inspector2, IResponseInspector2, IBaseInspector2
    {
        private bool mBDirty;
        private bool mBReadOnly;
        private byte[] mBody;
        private HTTPResponseHeaders mResponseHeaders;
        private JSONResponseViewer mJsonResponseViewer;

        public ResponseDecryptionFormat()
        {
            mJsonResponseViewer = new JSONResponseViewer();
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
                return mJsonResponseViewer.body;
            }

            set
            {
                mBody = value;
                byte[] decodedBody = DoDecryption();
                if (decodedBody != null)
                {
                    mJsonResponseViewer.body = decodedBody;
                }
                else
                {
                    mJsonResponseViewer.body = value;
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
                mJsonResponseViewer.bReadOnly = value;
            }
        }

        HTTPResponseHeaders IResponseInspector2.headers
        {
            get => mResponseHeaders;
            set
            {
                mResponseHeaders = value;
                mJsonResponseViewer.headers = value;
            }
        }

        public override void AddToTab(TabPage o)
        {
            mJsonResponseViewer.AddToTab(o);
            o.Text = "DecryptionJSON";
        }

        public void Clear()
        {
            mBody = null;
            mJsonResponseViewer.Clear();
        }

        // 在 Tab 上的摆放位置
        public override int GetOrder() => 100;
    }
}
