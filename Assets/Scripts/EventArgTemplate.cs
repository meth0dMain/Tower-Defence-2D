using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventArgTemplate<T> : EventArgs
{
   public T Attachment { get; private set; }

   public EventArgTemplate(T attachment)
   {
      Attachment = attachment;
   }

   public void SomeMethod()
   {
      var someBool = new EventArgTemplate<bool>(false);
      var someString = new EventArgTemplate<string>("");
      var someInt = new EventArgTemplate<int>(0);
      var someFloat = new EventArgTemplate<float>(0.0f);
   }
}
