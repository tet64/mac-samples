// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;

namespace MacDatabinding
{
	public partial class MainViewController : NSViewController
	{
		#region Private Variables
		private SubviewType _viewType = SubviewType.None;
		#endregion

		#region Computed Properties
		public MainWindowController WindowController { get; set;}

		public NSView Content {
			get { return ContentView; }
		}

		public NSViewController ContentController { get; set; }

		public SubviewType ViewType { 
			get { return _viewType; }
			set {
				_viewType = value;
				WindowController?.UpdateUI ();
			}
		}
		#endregion

		#region Constructors
		public MainViewController (IntPtr handle) : base (handle)
		{
		}
		#endregion

		#region Override Methods
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Populate Source List
			SourceList.Initialize ();

			var TableViews = new SourceListItem ("Data Binding Type");
			TableViews.AddItem ("Simple Binding", "shoebox.png", () => {
				ViewType = SubviewType.SimpleBinding;
				PerformSegue("SimpleSegue", this);
			});
			TableViews.AddItem ("Table Binding", "shoebox.png", () => {
				ViewType = SubviewType.TableBinding;
				PerformSegue("TableSegue", this);
			});
			TableViews.AddItem ("Outline Binding", "shoebox.png", () => {
				ViewType = SubviewType.OutlineBinging;
				PerformSegue("OutlineSegue", this);
			});
			TableViews.AddItem ("Collection View", "shoebox.png", () => {
				ViewType = SubviewType.CollectionView;
				PerformSegue("CollectionSegue", this);
			});
			SourceList.AddItem (TableViews);

			// Display Source List
			SourceList.ReloadData();
			SourceList.ExpandItem (null, true);
		}

		public override void PrepareForSegue (NSStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);


			// Take action based on type
			switch (segue.Identifier) {
			case "AddSegue":
				var editor = segue.DestinationController as PersonEditorViewController;
				editor.Presentor = this;
				editor.Person = new PersonModel();

				// Wire-up
				editor.PersonModified += (person) => {
					// Take action based on type
					switch(ViewType) {
					case SubviewType.TableBinding:
						var controller = ContentController as TableViewController;
						controller.AddPerson(person);
						break;
					case SubviewType.CollectionView:
						//					var collection = SubviewController as SubviewCollectionViewController;
						//					collection.EditPerson(this);
						break;
					}
				};
				break;
			default:
				// Save link to child controller
				ContentController = segue.DestinationController as NSViewController;
				break;
			}
		}
		#endregion
	}
}