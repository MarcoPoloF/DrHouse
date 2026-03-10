using MapKit;

namespace DocNoc.Xam.iOS
{
	public class CustomMKAnnotationView : MKAnnotationView
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Discipline { get; set; }

		public string ImageUrl { get; set; }

		public CustomMKAnnotationView(IMKAnnotation annotation, string id)
			: base(annotation, id)
		{
		}
	}
}