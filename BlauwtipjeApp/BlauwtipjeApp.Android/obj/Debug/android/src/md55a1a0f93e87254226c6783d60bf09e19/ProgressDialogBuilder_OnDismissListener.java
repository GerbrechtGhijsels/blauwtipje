package md55a1a0f93e87254226c6783d60bf09e19;


public class ProgressDialogBuilder_OnDismissListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.content.DialogInterface.OnDismissListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDismiss:(Landroid/content/DialogInterface;)V:GetOnDismiss_Landroid_content_DialogInterface_Handler:Android.Content.IDialogInterfaceOnDismissListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BlauwtipjeApp.Droid.Components.Dialog.ProgressDialogBuilder+OnDismissListener, BlauwtipjeApp.Android", ProgressDialogBuilder_OnDismissListener.class, __md_methods);
	}


	public ProgressDialogBuilder_OnDismissListener ()
	{
		super ();
		if (getClass () == ProgressDialogBuilder_OnDismissListener.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.Dialog.ProgressDialogBuilder+OnDismissListener, BlauwtipjeApp.Android", "", this, new java.lang.Object[] {  });
	}


	public void onDismiss (android.content.DialogInterface p0)
	{
		n_onDismiss (p0);
	}

	private native void n_onDismiss (android.content.DialogInterface p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
