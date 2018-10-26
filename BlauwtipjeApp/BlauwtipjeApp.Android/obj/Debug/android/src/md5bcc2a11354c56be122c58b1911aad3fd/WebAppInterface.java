package md5bcc2a11354c56be122c58b1911aad3fd;


public class WebAppInterface
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		java.lang.Runnable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_AndroidAlert:(Ljava/lang/String;)V:__export__\n" +
			"n_run:()V:GetRunHandler:Java.Lang.IRunnableInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("BlauwtipjeApp.Droid.Components.WebView.WebAppInterface, BlauwtipjeApp.Android", WebAppInterface.class, __md_methods);
	}


	public WebAppInterface ()
	{
		super ();
		if (getClass () == WebAppInterface.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.WebView.WebAppInterface, BlauwtipjeApp.Android", "", this, new java.lang.Object[] {  });
	}

	public WebAppInterface (android.content.Context p0)
	{
		super ();
		if (getClass () == WebAppInterface.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.WebView.WebAppInterface, BlauwtipjeApp.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void androidAlert (java.lang.String p0)
	{
		n_AndroidAlert (p0);
	}

	private native void n_AndroidAlert (java.lang.String p0);


	public void run ()
	{
		n_run ();
	}

	private native void n_run ();

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
