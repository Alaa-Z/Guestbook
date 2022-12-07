using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
/**
 * Console application for a guestbook with the possibility
 * to add a post, delete a post by its index and display all posts
 * Adding a post give the choice to enter the user name and the post itself.
 * These fields must not be empty.
 * All posts must be serialized/deserialized and saved to file as json
 * @author  Alaa Zaza
 * @version 1.0
 * @since   2022-12-06
 */
namespace Program{
    public class Post {
       
        // Short hand property for user with get and a set method
        public string User 
        { get; set; }
        
        // Short hand property for post content with get and a set method
        public string Content 
        { get; set; }
    }

    public class Guestbook {
        // file name to save all posts
        private string filename = @"post.json";
        
        // List called posts of Post type 
        private List<Post> posts = new List<Post>();

        /**
        Constructor that will read stored json data in the file if it exists
        */       
        public Guestbook(){ 
            if(File.Exists(@"post.json")==true) { 
                string jsonString = File.ReadAllText(filename);
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }

        /**
        Function to add post that the user entered 
        @param the user name as string  
        @param the post content as string
        @return post 
        */
        public Post addPost(string u ,string c){
            //Create an instance of the class 
            Post post= new Post();
            post.User = u;
            post.Content = c;
            // add the post in the list
            posts.Add(post);
            // call the function 
            marshal();         
            return post;
        }

        /**
        Function to delete post by its index
        @param the post index 
        @return the index(int)
        */
        public int deletePost(int index) {
        posts.RemoveAt(index);
        marshal();
        return index;
        }

        /**
        Function to get all posts
        @return a list of posts
        */
        public List<Post> getPosts() {
            return posts;
        }

        /**
        Function to Serialize all the objects and save it to file
        */
        private void marshal() {
            var jsonString = JsonSerializer.Serialize(posts);
            Console.WriteLine("::"+jsonString);
            File.WriteAllText(filename, jsonString);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Create an instance of the class 
            Guestbook guestbook = new Guestbook();
            // Conter to match with indexes 
            int i = 0;

            while(true){
                // clear the console
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("A L A A ' S   G U E S T B O O K\n\n");

                Console.WriteLine("1. Skriv i Gästboken");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. För att Avsluta\n");

                i = 0;
                // to print all posts
                foreach(Post post in guestbook.getPosts()){
                    Console.WriteLine( "[" + i++ + "] " + post.User + " - " +post.Content);
                }

                // read the input from user
                int input = (int) Console.ReadKey(true).Key;
               
                switch (input) {
                    case '1': // to add post
                        Console.CursorVisible = true; 

                        Console.Write("Lägg till användare namn: ");
                        // read the first input 
                        string userInput1 = Console.ReadLine();
                        // check if it is not empty 
                        if(!String.IsNullOrEmpty(userInput1)) {
                            Console.Write("Lägg till det nya inlägget: ");
                            // read the second input 
                            string userInput2 = Console.ReadLine();
                            // check if it is not empty 
                            if(!String.IsNullOrEmpty(userInput2)) 
                            // add the post then
                            guestbook.addPost(userInput1, userInput2);
                        }
                        break;

                    case '2': // to delete post
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        // Read the index from user
                        string index = Console.ReadLine();
                        // check if it is not empty 
                        if(!String.IsNullOrEmpty(index))
                            try{
                                // delete the post with that index(int)
                                guestbook.deletePost(Convert.ToInt32(index));
                            }
                            catch(Exception e){ // to catch the error
                                Console.WriteLine("Index out of range!\nPress button to proceed.");
                                Console.ReadKey();
                            }
                        break;
                    case 88:  // to exit 
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

