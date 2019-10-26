using Fiddler;
using System;
using Standard;
using System.Windows.Forms;
using Util;

namespace Request
{
    public sealed class RequestDecryption : Inspector2, IRequestInspector2, IBaseInspector2
    {
        private bool mBDirty;
        private bool mBReadOnly;
        private byte[] mBody;
        private HTTPRequestHeaders mRequestHeaders;
        private RequestTextViewer mRequestTextViewer;

       public RequestDecryption()
        {
            mRequestTextViewer = new RequestTextViewer();
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
                    mRequestTextViewer.body = decodedBody;
                }
                else
                {
                    mRequestTextViewer.body = value;
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

        public HTTPRequestHeaders headers
        {
            get
            {
                FiddlerApplication.Log.LogString("headers get function.");
                return mRequestHeaders;
            }  
            set
             {
                mRequestHeaders = value;           

            }

        }

        public override void AddToTab(TabPage o)
        {
            mRequestTextViewer.AddToTab(o);
            o.Text = "Decryption";
        }

        public  void Clear()
        {
            mBody = null;
            mRequestTextViewer.Clear();
        }

        // 在 Tab 上的摆放位置
        public override int GetOrder() => 100;
    }
}
