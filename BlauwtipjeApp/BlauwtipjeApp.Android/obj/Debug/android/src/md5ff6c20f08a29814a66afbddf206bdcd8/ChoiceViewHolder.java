package md5ff6c20f08a29814a66afbddf206bdcd8;


public class ChoiceViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BlauwtipjeApp.Droid.Components.RecyclerListView.ViewHolders.ChoiceViewHolder, BlauwtipjeApp.Android", ChoiceViewHolder.class, __md_methods);
	}


	public ChoiceViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ChoiceViewHolder.class)
			mono.android.TypeManager.Activate ("BlauwtipjeApp.Droid.Components.RecyclerListView.ViewHolders.ChoiceViewHolder, BlauwtipjeApp.Android", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
