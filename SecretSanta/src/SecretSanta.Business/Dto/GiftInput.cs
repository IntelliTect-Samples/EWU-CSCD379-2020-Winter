﻿namespace SecretSanta.Business.Dto
{
    public class GiftInput
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
