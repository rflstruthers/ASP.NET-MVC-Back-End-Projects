# Live Project

## Introduction
For the last two weeks of my time at the tech academy, I worked with my peers in a team developing a full scale MVC/MVVM Web Application in C#. Working on a legacy codebase was a great learning oppertunity for fixing bugs, cleaning up code, and adding requested features. I saw how a good developer works with what they have to make a quality product. I worked on several [back end stories](#back-end-stories) that I am very proud of and was abale to gain experience both adding a new feature for the application and updating existing code. Over the two week sprint I also had the opportunity to work on some other project management and team programming [skills](#other-skills-learned) that have made me a better developer.

Below are descriptions of the stories I worked on, along with code snippets and navigation links.

## Back End Stories
### Products and Shopping Cart
I was tasked with creating a page that displays various products for sale along with a shopping cart that is associated with each specific user of the application. I created two Model classes: Product and CartItem with appropriate parameters.

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
    }
    
     public class CartItem
    {
        [Key]
        public string CartItemId { get; set; }
        public string User { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
    
  I then created a Controller each for Product and CartItem. The product Controller has methods to add product to the cart and view details of the product. If the user is an administrator, there are methods for creating, editing, and deleting  
  











## Other Skills Learned
* Working with a group of developers to identify front and back end bugs to the improve usability of an application
* Improving project flow by communicating about who needs to check out which files for their current story
* Learning new efficiencies from other developers by observing their workflow and asking questions  
* Practice with team programming/pair programming when one developer runs into a bug they cannot solve
* One of the developers on the team was having trouble with the JavaScript function being called to increment and decrement the likes on a page and myself and two others on the team sat with him and had him talk through what he had done so far. I asked questions about different ways to approach it until we found where it was broken and what needed to be fixed.
* When a user requests a friendship there is supposed to be a pending notification displayed. One of the other developers was hitting a wall while working on this story when he discovered the functionality was working four different ways across the application. I sat with him and we talked through the process of each JavaScript function being called. We discovered there were multiple functions by the same name being loaded, so we simplified the code down to just one function. Clicking the button would now work from the nav drop-down but not on a specific page. I realized that the page was populating two different spans with the same ID and these were what was being targeted by the JavaScript function. So we needed to make that user-specific element identifier a class and target the class instead so that a change in either place would affect both.
  
*Jump to: [Back End Stories](#back-end-stories), [Page Top](#live-project)*
