﻿namespace Books.API.Models;

public class BookCoverDto
{
    public string Id { get; set; }
    //public byte[]? Content { get; set; }

    public BookCoverDto(string id/*, byte[]? content*/)
    {
        Id = id;
        //Content = content;
    }
}
