namespace AllSpice.Models
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }

        public int RecipeId { get; set; }
    }
}