# FiddlerDecryption

功能：实时解密接口数据并格式化展示。

代码内容：

* DecryptionUtil.cs：解密方法
* RequestDecryption.cs：请求数据的解密插件
* RequestDecryptionFormat.cs：请求数据的解密&格式化插件
* ResponseDecryption.cs：返回数据的解密插件
* ResponseDecryptionFormat.cs：返回数据的解密&格式化插件

如需应用到自己的项目，修改 DecryptionUtil 中的解密算法即可。

更多介绍请见[Fiddler 插件开发：数据解密](https://blog.csdn.net/Gdeer/article/details/102756017)。
