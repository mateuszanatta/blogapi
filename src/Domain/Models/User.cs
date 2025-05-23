﻿namespace Domain.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public ICollection<BlogPost> BlogPosts { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
}
