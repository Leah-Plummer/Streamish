﻿using Streamish.Models;
using System;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IVideoRepository
    {
        void Add(Video video);
        void Delete(int id);
        Video GetById(int id);
        List<Video> GetAll();
        public List<Video> GetAllWithComments();

        public Video GetVideoByIdWithComments(int id);
        void Update(Video video);
        public List<Video> Search(string criterion, bool sortDescending);

        public List<Video> Hottest(DateTime since);
    }
}