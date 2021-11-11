# two_code
一个关于android for tensorflow,1个关于arcengine的二次开发（arcscene）
xu是二次开发，包括一些axscenecontrol控件的一些代码
xu_first是用tensorflow的python代码

## pytorch Android
### 转换时报错
1. Cannot input a tensor of dimension other than 0 as a scalar argument。这种一般是多余的包装。如`torch.tensor(pts[:2, :].clone())`可以正常工作，但是在Android中会报该错，正确是`pts[:2, :].clone()`。原因是pts已经是一个tensor了。所以不用包装。
2. com.facebook.jni.CppException: tensors used as indices must be long, byte or bool tensors。下标必须为指定类型。
   
   ```
   # inds = torch.argsort(pts[2, :], descending=True) # 报错
   inds = torch.argsort(pts[2, :], descending=True).long()
   pts = pts[:, inds]
   ```
4. 
