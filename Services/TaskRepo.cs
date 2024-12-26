using DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskRepo: ITask
    {
        private readonly AppDbContext _context;

        public TaskRepo(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskDto> GetAllTasks()
        {
            return _context.Tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Title,
                Description = t.Description,
                Type = "General",
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted
            }).ToList();
        }

        public TaskDto GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Name = task.Title,
                Description = task.Description,
                Type = "General",
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted
            };
        }

        public void AddTask(TaskDto taskDto)
        {
            var task = new TaskModel
            {
                Title = taskDto.Name,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                IsCompleted = taskDto.IsCompleted
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskDto taskDto)
        {
            var task = _context.Tasks.Find(taskDto.Id);
            if (task == null) return;

            task.Title = taskDto.Name;
            task.Description = taskDto.Description;
            task.DueDate = taskDto.DueDate;
            task.IsCompleted = taskDto.IsCompleted;

            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
