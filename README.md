# Live Project

## Introduction
During my internship at Prosper IT Consulting, I worked with my peers in a team developing a full scale MVC/MVVM Web Application in C#. For a two week sprint, I worked on the back end of the project. Working on a legacy codebase was a great learning oppertunity for fixing bugs, cleaning up code, and adding requested features. I saw how a good developer works with what they have to make a quality product. I worked on several [back end stories](#back-end-stories) that I am very proud of and was abale to gain experience both adding a new feature for the application and updating existing code. Over the two week sprint I also had the opportunity to work on some other project management and team programming [skills](#other-skills-learned) that have made me a better developer.

Below are descriptions of the stories I worked on, along with code snippets and navigation links. Some of the full code files I worked on are in this repo.

## Back End Stories
### Products and Shopping Cart
I was tasked with creating a page that displays various products for sale along with a shopping cart that is associated with each specific user of the application. I created two Model classes: Product and CartItem with appropriate parameters. These classes are reflected in a database using Entity Framework Code First.
   
I then created a Controller each for Product and CartItem. The product Controller has methods to acess the products from the database and pass them to the Views as well as displaying the details of the product. If the user is an administrator, there are methods for creating, editing, and deleting products.

        // Returns a list of the products to the Index view
        // If the user is the Admin, redirects to the AdminIndex method below
        public ActionResult Index()
        {
            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var s = UserManager.GetRoles(user.GetUserId());

            if (s[0].ToString() == "Admin")
            {
                return RedirectToAction("AdminIndex");
            }
            else
            {
                return View(db.Products.ToList());
            } 
        }

The CartItem Controller has methods for adding an item to the cart, displaying the items in the cart, increasing and decreasing the quantity of a product, as well as deleting a product from the cart and viewing the product's details. I had to determine the user's identity in order to make the CartItem object specific to the user. The AddToCart method checks whether the selected Product is already in the CartItems database table, if it isn't then the Product is added as a CartItem, if it is, then the Quantity property is increased by one.

        // Add selected Product to CartItems
        // Check if Product already exists in CartItems, if so then add 1 to the Quantity, 
        // if not then create a new CartItem and add it to the CartItems table
        public ActionResult AddToCart(int id)
        {
            var userId = User.Identity.GetUserId().ToString();
            var cartItem = db.CartItems.SingleOrDefault(c => c.ProductId == id && c.User == userId);
            if (cartItem == null)
            {
                var cartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        CartItemId = Guid.NewGuid().ToString(),
                        User = userId,
                        Quantity = 1,
                        DateCreated = DateTime.Now,
                        ProductId = id,
                        Product = db.Products.SingleOrDefault(p => p.ProductId == id)
                    }
                };
                cartItems.ForEach(c => db.CartItems.Add(c));
            }
            else
            {
                cartItem.Quantity++;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        
I created Views for Product and CartItem to display the products for sale as well as the shopping cart for that user.

        @model IEnumerable<ManagementPortal.Models.Product>
        @{
            ViewBag.Title = "Index";
        }
        <h2>Products</h2>
        <div class="card mx-2 px-3 py-1">
            <p>
                @Html.ActionLink("View Cart", "Index", "CartItem")
            </p>
            <div class="container">
                <div class="row">
                    @foreach (var item in Model)
                    {
                        <div class="col-sm-6 col-lg-3 py-2">
                            <div class="card">
                                <img src="~/Content/images/Product/@(item.ImagePath)" class="card-img-top mx-auto" alt="Image"                                              style="width:210px;height:200px;" />
                                <div class="card-body">
                                    <h5 class="card-title">@(Html.DisplayFor(modelItem => item.ProductName))</h5>
                                    <p class="card-text">$@(Html.DisplayFor(modelItem => item.UnitPrice))</p>
                                    <p class="card-text"><small class="text-muted">@(Html.DisplayFor(modelItem =>                                                               item.ProductDescription))</small></p>
                                    <p class="card-text">
                                        @Html.ActionLink("Add To Cart", "AddToCart", "CartItem", new { id = item.ProductId }, null) |
                                        @Html.ActionLink("Details", "Details", new { id = item.ProductId })
                                    </p>
                                </div>
                            </div>
                        </div>
                    }
                </div>
            </div>
        </div>

Creating new Model classes tought me the importance of planning before starting to code. I had to think carefully about what properties the classes I created would need and how the two classes would interact with eachother. Having a well thought out plan enabled me to be more efficient with my code writing and produce a better result in less time.

### Chat Message Edit
The application had a chat message functionality, with the administrator able to edit all messages. As it was, the administrator could edit the date the message was sent and the user that sent it as well as the content of the message. I was tasked with changing the edit page so only the message content could be edited. I had to make a list of the ChatMessage properties I didn't want to be changed in order to have the method only affect the message content. I also changed the View associated with the method to show the Date and Sender as read-only.

        // Makes a list of ChatMessage properties that we don't want changed so only the Message property can be edited.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatMessageId,Sender,Date,Message")] ChatMessage chatMessage)
        {
            if (ModelState.IsValid)
            {
                var excluded = new[] { "Date", "Sender" };
                var entry = db.Entry(chatMessage);
                entry.State = EntityState.Modified;
                foreach (var name in excluded)
                {
                    entry.Property(name).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chatMessage);
        }
        
Working on an existing part of the application gave me valuable experience troubleshooting code written by others. I debugged the existing code to determine how it was working and then made the necessary edits to fulfill the Story requirements. This was important practice in development that I will undoubtedly use many times in my future projects.

## Other Skills Learned
* Working with a group of developers to identify bugs to the improve usability of an application.
* Improving project flow by communicating about who needs to check out which files for their current story.
* Learning new efficiencies from other developers by observing their workflow and asking questions.  
* Practice with team programming/pair programming when one developer runs into a bug they cannot solve.
* Participating in daily Stand-Up's to update the Project Manager and peers on my work.
* Planning how to complete a User Story and implementing that plan to satify the Story requirements.
* Producing results by a specified deadline.
  
*Jump to: [Back End Stories](#back-end-stories), [Page Top](#live-project)*
