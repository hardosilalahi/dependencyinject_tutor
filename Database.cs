using System.Runtime.Intrinsics.X86;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Npgsql;
using webapi_DependenInject.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace webapi_DependenInject
{
    public interface IDatabase{
        List<Posts> readPost();
        int createPost(Posts post);
        Posts readByID(int id);
        void updatePost(int id, [FromBody]JsonPatchDocument<Posts> data);
        void deletePost(int id);
    }
    public class Database : IDatabase
    {
         NpgsqlConnection _connection;
         
        //  private string guid { get; set; }

        public Database(NpgsqlConnection connection){

            _connection = connection;
            _connection.Open();
        }

        public List<Posts> readPost(){
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM posts";
            var result = command.ExecuteReader();
            var Posts = new List<Posts>();
            while (result.Read())
                Posts.Add(new Posts() { 
                    Id = (int)result[0], 
                    Title = (string)result[1], 
                    Content = (string)result[2] ,
                    Tags = (string)result[3], 
                    Status = (bool)result[4] ,
                    Create_Time = (DateTime)result[5], 
                    Update_Time = (DateTime)result[6] ,
                });
            _connection.Close();
            return Posts;

        }

        public int createPost(Posts post){
            var command = _connection.CreateCommand();

            command.CommandText = "INSERT INTO Posts (title,content,tags,status,create_time,update_time) VALUES (@title , @content, @tags, @status, current_timestamp, current_timestamp) RETURNING id";
            command.Parameters.AddWithValue("@title", post.Title);
            command.Parameters.AddWithValue("@content", post.Content);
            command.Parameters.AddWithValue("@tags", post.Tags);
            command.Parameters.AddWithValue("@status", post.Status);
            // command.Parameters.AddWithValue("@create_time", post.Create_Time);
            // command.Parameters.AddWithValue("@update_time", post.Update_Time);

            command.Prepare();
            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }

        public Posts readByID(int id){
            var command = _connection.CreateCommand();

            command.CommandText = $"SELECT * FROM Posts WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);
            var result = command.ExecuteReader();
            result.Read();

            var postsId = new Posts(){
                    Id = (int)result[0], 
                    Title = (string)result[1], 
                    Content = (string)result[2] ,
                    Tags = (string)result[3], 
                    Status = (bool)result[4] ,
                    Create_Time = (DateTime)result[5], 
                    Update_Time = (DateTime)result[6]
            };
            _connection.Close();
            return postsId;
        }

        public void updatePost(int id, [FromBody]JsonPatchDocument<Posts> data){
            var command = _connection.CreateCommand();
            var postsId = readByID(id);
            _connection.Open();

            data.ApplyTo(postsId);

            command.CommandText = $"UPDATE Posts SET(title, content, tags, status, create_time, update_time) = (@title, @content, @tags, @status, @create_time, current_timestamp) WHERE id = {id}";
            command.Parameters.AddWithValue("@title", postsId.Title);
            command.Parameters.AddWithValue("@content", postsId.Content);
            command.Parameters.AddWithValue("@tags", postsId.Tags);
            command.Parameters.AddWithValue("@status", postsId.Status);
            command.Parameters.AddWithValue("@create_time", postsId.Create_Time);
            // command.Parameters.AddWithValue("@update_time", post.Update_Time);

            command.ExecuteNonQuery();
            _connection.Close();
        }


        public void deletePost(int id){
            var command = _connection.CreateCommand();

            command.CommandText = $"DELETE FROM posts WHERE id = {id}";

            var result = command.ExecuteNonQuery();
            _connection.Close();

        }
    }
}