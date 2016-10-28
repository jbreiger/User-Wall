using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using wall2.Models;
using Microsoft.Extensions.Options;

namespace wall2.Factory
{
    public class MessageFactory : IFactory<User>
    {
        // private string connectionString;

        // public UserFactory()
        // {
        //     connectionString = "server=localhost;UserId=root;password=root;port=8889;database=login;SslMode=None";
        // }

         private readonly IOptions<MySqlOptions> mysqlConfig;

        public MessageFactory(IOptions<MySqlOptions> conf) {
            mysqlConfig = conf;
        }

        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
         public void Add(Message item){
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO messages (message, user_id, created_at, updated_at) VALUES (@message, @user_id, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

         public void AddComment(int message_id, string comment, int user_id){
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO comments (comment, message_id, user_id, created_at, updated_at) VALUES ('"+comment +"', '"+ message_id + "', '"+ user_id + "', NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

        public IEnumerable<Message> FindAll()
{
    using (IDbConnection dbConnection = Connection)
    {
        var query = $"select * from messages left join users on messages.user_id = users.id";
        dbConnection.Open();
        var mymessages = dbConnection.Query<Message, User, Message>(query, (message, user) => { message.user = user; return message; });
        return mymessages;
    }
}

        public IEnumerable<Comment> FindAllComments()
{
    using (IDbConnection dbConnection = Connection)
    {
        var query = $"select * from comments left join users on comments.user_id = users.id";
        dbConnection.Open();
        var mycomments = dbConnection.Query<Comment, User, Comment>(query, (comment, user) => { comment.user = user; return comment; });
        return mycomments;
    }
}








        //  public User FindAll()
        // {
        //     using (IDbConnection dbConnection =Connection)
        //     {
        //         dbConnection.Open();
        //         var query = 
        //         @"
        //         select * from messages;
        //         select * from users;";
        //         using (var multi = dbConnection.QueryMultiple(query)){
        //             var user = multi.Read<User>().Single();
        //             user.messages = multi.Read<Message>().ToList();
        //             return user;
                    
        //         }

               
        //     }
        // }

        // public IEnumerable<Message> FindAll()
        // {
        //     using (IDbConnection dbConnection =Connection)
        //     {
        //         dbConnection.Open();
        //         return dbConnection.Query<Message>( "Select * from messages");
        //     }
        // }
        
    }
}       
    