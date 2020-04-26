using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SafeEventHandler
{
   public static void SafelyBroadcastEvent<T>(ref EventHandler<EventArgTemplate<T>> eventToBroadcast, T dataToAttach, object sender)
   {
      if (eventToBroadcast != null)
      {
         var attachment = new EventArgTemplate<T>(dataToAttach);
         eventToBroadcast.Invoke(sender, attachment);
      }
   }

   public static void SafelyBroadcastAction(ref Action actionToBroadcast)
   {
      if (actionToBroadcast != null)
      {
         actionToBroadcast.Invoke();
      }
   }
}
