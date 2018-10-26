package md584b3ab5484a30fa67d7410ced5d9fa65;


public class ObjectExtensions_JavaHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BlauwtipjeApp.Droid.Classes.ObjectExtensions+JavaHolder, BlauwtipjeApp.Android", ObjectExtensions_JavaHolder.class, __md_methods);
	}


	public ObjectExtensions_JavaHolder ()
	{
		super ();
		if (getClass () == ObjectExtensions_JavaHolder.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Classes.ObjectExtensions+JavaHolder, BlauwtipjeApp.Android", "", this, new java.lang.Object[] {  });
	}

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
