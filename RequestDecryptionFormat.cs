using Fiddler;
using System;
using System.Windows.Forms;
using Standard;
using Util;

namespace Request
{
    public class RequestDecryptionFormat : Inspector2, IRequestInspector2, IBaseInspector2
    {
        private bool mBDirty;
        private bool mBReadOnly;
        private byte[] mBody;
        private HTTPRequestHeaders mRequestHeaders;
        private JSONRequestViewer mJsonRequestViewer;

        public RequestDecryptionFormat()
        {
            mJsonRequestViewer = new JSONRequestViewer();
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
                return mJsonRequestViewer.body;
            }

            set
            {
                mBody = value;
                byte[] decodedBody = DoDecryption();
                if (decodedBody != null)
                {
                    mJsonRequestViewer.body = decodedBody;
                }
                else
                {
                    mJsonRequestViewer.body = value;
                }
            }
        }

        public byte[] DoDecryption()
        {
            // 将 byte[] 转成字符串
            String rawBoday = System.Text.Encoding.Default.GetString(mBody);
            String showBody = rawBoday;
            if (!rawBoday.Contains("{"))
            {
                // 需要解密
                FiddlerApplication.Log.LogString("rawBoday: " + rawBoday);
                showBody = DecryptionUtil.DecryptSDKBody(rawBoday);
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
                mJsonRequestViewer.bReadOnly = value;
            }
        }

        public HTTPRequestHeaders headers
        {
            get
            {
                return mRequestHeaders;
            }

            set
            {

                mRequestHeaders = value;
                mJsonRequestViewer.headers = value;
            }
        }

        public override void AddToTab(TabPage o)
        {
            mJsonRequestViewer.AddToTab(o);
            o.Text = "DecryptionJSON";
        }

        public void Clear()
        {
            mBody = null;
            mJsonRequestViewer.Clear();
        }

        // 在 Tab 上的摆放位置
        public override int GetOrder() => 100;
    }
}
