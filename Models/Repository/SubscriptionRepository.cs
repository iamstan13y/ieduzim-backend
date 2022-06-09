﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Subscription>> AddAsync(Subscription subscription)
        {
            try
            {
                await _context.Subscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();

                subscription.LessonSchedules = new();
                subscription.LessonScheduleIds.ForEach(id =>
                {
                    var lessonSchedule = _context.LessonSchedules.Where(x => x.Id == id).FirstOrDefault();

                    lessonSchedule.SubscriptionId = subscription.Id;
                    subscription.LessonSchedules.Add(lessonSchedule);
                });

                await _context.SaveChangesAsync();

                return new Result<Subscription>(subscription);
            }
            catch (Exception)
            {
                return new Result<Subscription>(false, "Failed to create!", null);
            }
        }

        public async Task<Result<IEnumerable<Subscription>>> GetAllAsync()
        {
            var subscriptions = await _context.Subscriptions
                .Include(x => x.Payment)
                .ToListAsync();
            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        public async Task<Result<Subscription>> GetByIdAsync(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(x => x.Payment)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (subscription == null) return new Result<Subscription>(false, "Subscription not found", null);
            return new Result<Subscription>(subscription);
        }

        public async Task<Result<IEnumerable<Subscription>>> GetByStudentIdAsync(int studentId)
        {
            var subscriptions = await _context.Subscriptions
                .Where(x => x.StudentId == studentId)
                .Include(x => x.Payment)
                .ToListAsync();

            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        //rhetoric
        public async Task<Result<IEnumerable<Subscription>>> GetByTeacherIdAsync(int teacherId)
        {
            var subscriptions = await _context.Subscriptions.ToListAsync();

            return new Result<IEnumerable<Subscription>>(subscriptions);
        }
    }
}