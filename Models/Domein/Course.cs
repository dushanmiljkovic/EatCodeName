namespace Models.Domein
{
    //A three-course meal usually has an appetizer, a main course, and a dessert.
    //A four course meal might include a soup, an appetizer, a main course, and dessert.
    //A five course meal can include a soup, an appetizer, a salad, a main course, and a dessert.
    //A six course meal usually includes an amuse-bouche, a soup, an appetizer, a salad, a main course, and a dessert.
    //A seven course meal includes an amuse-bouche, a soup, an appetizer, a salad, a main course, a dessert, and a mignardise with coffee or tea.
    internal class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}