# FiddlerDecryption

功能：实时解密接口数据并格式化展示。

![](https://img-blog.csdnimg.cn/20191026140952602.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L0dkZWVy,size_16,color_FFFFFF,t_70)

代码内容：

* DecryptionUtil.cs：解密方法
* RequestDecryption.cs：请求数据的解密插件
* RequestDecryptionFormat.cs：请求数据的解密&格式化插件
* ResponseDecryption.cs：返回数据的解密插件
* ResponseDecryptionFormat.cs：返回数据的解密&格式化插件

如需应用到自己的项目，修改 DecryptionUtil 中的解密算法即可。

更多介绍请见[Fiddler 插件开发：数据解密](https://blog.csdn.net/Gdeer/article/details/102756017)。
