using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using EmpyreBookApp.Models;
using EmpyreBookApp.DAL;

namespace EmpyreBookApp.GoogleREST
{
    public class BookSearcher
    {
        private RequestContext db = new RequestContext();

        public GoogleREST.Book[] searchISBN(string isbn)
        {
            string bookRequest = createRequest(isbn);
            Response booksResponse = makeRequest(bookRequest);
            if (booksResponse == null)
            {
                throw new Exception("Response was empty");
            }
            return booksResponse.items;
        }

        public string createRequest(string isbn)
        {
            string UrlRequest = "https://www.googleapis.com/books/v1/volumes?q=" + isbn + "+isbn";
            return (UrlRequest);
        }

        public Response makeRequest(string requestUrl)
        {
            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                StringBuilder jsonResponse = new StringBuilder();
                Stream receiveStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[256];
                // Reads 256 characters at a time.     
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    jsonResponse.Append(str);
                    count = readStream.Read(read, 0, 256);
                }
                Response ret = JsonConvert.DeserializeObject<Response>(jsonResponse.ToString());
                return ret;
            }
        }

        public BookInfo parse2BI(Book b1, string isbn)
        {
            VolumeInfo v = b1.volumeInfo;
            StringBuilder authors = new StringBuilder();
            foreach (string auth in v.authors)
            {
                authors.Append(auth + ", ");
            }
            authors.Remove(authors.Length - 2, 2);
            return new BookInfo
            {
                Title = v.title,
                Authors = authors.ToString(),
                CoverLink = v.imageLinks.thumbnail,
                Version = v.publishedDate,
                ISBN = isbn
            };
        }

        public int DbSearch(string isbn)
        {
            var bis = from b in db.BIs
                      where b.ISBN.Equals(isbn)
                      select b;
            if (bis.Count() != 0)
            {
                return bis.First().BIID;
            }
            EmpyreBookApp.GoogleREST.Book[] books = searchISBN(isbn);
            EmpyreBookApp.GoogleREST.Book b1 = books.First<EmpyreBookApp.GoogleREST.Book>();
            BookInfo model = parse2BI(b1, isbn);
            BI bi = new BI { Authors = model.Authors, CoverLink = model.CoverLink, ISBN = model.ISBN, Title = model.Title, Version = model.Version };
            db.BIs.Add(bi);
            db.SaveChanges();
            return bi.BIID;
        }
    }
}