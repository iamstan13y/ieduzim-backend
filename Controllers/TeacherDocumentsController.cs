//using IEduZimAPI.CoreClasses;
//using IEduZimAPI.Models;
//using IEduZimAPI.Models.Data;
//using IEduZimAPI.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IEduZimAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize]
//    public class TeacherDocumentsController:BaseController<TeacherDocuments, Models.Local.TeacherDocument>
//    {
//        private TeacherDocumentsService service;
//        public TeacherDocumentsController() => service = new TeacherDocumentsService();

//        [HttpGet]
//        [Route("by-user-id/{userId}")]
//        public Result<IEnumerable<TeacherDocuments>> Get(string userId) =>
//            ExecutionService<IEnumerable<TeacherDocuments>>.Execute(() => service.GetByUserId(userId)); 

//        [HttpGet]
//        [Route("authenticate/{userId}/{documentId}")]
//        public Result<TeacherDocuments> AuthenticateDocuments(string userId, int documentId) =>
//            ExecutionService<TeacherDocuments>.Execute(() => service.AuthenticateDocument(userId, documentId));

//        [HttpPut]
//        [Route("update-verification/{userId}")]
//        public Result<bool> VerifyTeacher(string userId) =>
//            ExecutionService<bool>.Execute(() => service.CheckAllAuthenticated(userId));
//    }
//} 
