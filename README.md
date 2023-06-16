# iml6yu.MessageBus
 消息总线

 ## demo
 
 ### 发布消息
```c#
  MessageBuses.Publish<MyClass1>(new MyClass1());
```
 ### 订阅消息
 
```c#
   ISubscriber<MyClass1> subscriber;
   subscriber = MessageBuses.Subscrib<MyClass1>("newWin");
    subscriber.OnNoticed += (channel,e)=>{
       Debug.WriteLine(e.Data.ToString())  
    };
```

