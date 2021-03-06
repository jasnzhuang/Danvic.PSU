﻿//-----------------------------------------------------------------------
// <copyright file= "NewbornRepository.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Date Created: 2018/4/12 星期四 19:32:49
// Modified by:
// Description: Instructor-Newborn-功能实现仓储
//-----------------------------------------------------------------------
using LinqKit;
using Microsoft.EntityFrameworkCore;
using PSU.EFCore;
using PSU.Entity.Basic;
using PSU.Entity.SignUp;
using PSU.Model.Areas.Instructor.Newborn;
using PSU.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PSU.Repository.Areas.Instructor
{
    public class NewbornRepository
    {
        #region Register API

        /// <summary>
        /// 根据搜索条件获取新生报名信息
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<Register>> GetListAsync(RegisterViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SMajorClass) && string.IsNullOrEmpty(webModel.SDate))
            {
                return await context.Set<Register>().Where(i => i.InstructorId == CurrentUser.UserId).Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.DateTime).ToListAsync();
            }
            else
            {
                IQueryable<Register> registers = context.Register.AsQueryable();

                var predicate = PredicateBuilder.New<Register>();

                //当前登录人数据
                predicate = predicate.And(i => i.InstructorId == CurrentUser.UserId);

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.Name == webModel.SName);
                }

                //专业班级名称
                if (!string.IsNullOrEmpty(webModel.SMajorClass))
                {
                    predicate = predicate.And(i => i.MajorClass.Contains(webModel.SMajorClass));
                }

                //预计到校时间
                if (!string.IsNullOrEmpty(webModel.SDate))
                {
                    predicate = predicate.And(i => i.ArriveTime.ToString("yyyy-MM-dd") == webModel.SDate);
                }

                return await registers.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        /// <summary>
        /// 根据搜索条件获取新生报名信息列表个数
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<int> GetListCountAsync(RegisterViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SMajorClass) && string.IsNullOrEmpty(webModel.SDate))
            {
                var list = await context.Set<Register>().Where(i => i.InstructorId == CurrentUser.UserId).Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.DateTime).ToListAsync();
                return list.Count();
            }
            else
            {
                IQueryable<Register> registers = context.Register.AsQueryable();

                var predicate = PredicateBuilder.New<Register>();

                //当前登录人数据
                predicate = predicate.And(i => i.InstructorId == CurrentUser.UserId);

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.Name == webModel.SName);
                }

                //专业班级名称
                if (!string.IsNullOrEmpty(webModel.SMajorClass))
                {
                    predicate = predicate.And(i => i.MajorClass.Contains(webModel.SMajorClass));
                }

                //预计到校时间
                if (!string.IsNullOrEmpty(webModel.SDate))
                {
                    predicate = predicate.And(i => i.ArriveTime.ToString("yyyy-MM-dd") == webModel.SDate);
                }

                var list = await registers.AsExpandable().Where(predicate).ToListAsync();
                return list.Count();
            }
        }

        #endregion

        #region Dormitory API

        /// <summary>
        /// 根据搜索条件获取新生宿舍选择信息
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<BunkInfo>> GetListAsync(DormitoryViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SStudent) && string.IsNullOrEmpty(webModel.SBuilding))
            {
                return await context.Set<BunkInfo>().Where(i => i.InstructorId == CurrentUser.UserId).Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.DateTime).ToListAsync();
            }
            else
            {
                IQueryable<BunkInfo> bunkInfos = context.BunkInfo.AsQueryable();

                var predicate = PredicateBuilder.New<BunkInfo>();

                //当前登录人数据
                predicate = predicate.And(i => i.InstructorId == CurrentUser.UserId);

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SStudent))
                {
                    predicate = predicate.And(i => i.StudentName == webModel.SStudent);
                }

                //寝室号
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.DormName == webModel.SName);
                }

                //寝室楼
                if (!string.IsNullOrEmpty(webModel.SBuilding))
                {
                    predicate = predicate.And(i => i.BuildingName == webModel.SBuilding);
                }

                return await bunkInfos.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        /// <summary>
        /// 根据搜索条件获取新生宿舍选择信息列表个数
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<int> GetListCountAsync(DormitoryViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SStudent) && string.IsNullOrEmpty(webModel.SBuilding))
            {
                var list = await context.Set<BunkInfo>().Where(i => i.InstructorId == CurrentUser.UserId).Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.DateTime).ToListAsync();
                return list.Count();
            }
            else
            {
                IQueryable<BunkInfo> bunkInfos = context.BunkInfo.AsQueryable();

                var predicate = PredicateBuilder.New<BunkInfo>();

                //当前登录人数据
                predicate = predicate.And(i => i.InstructorId == CurrentUser.UserId);

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SStudent))
                {
                    predicate = predicate.And(i => i.StudentName == webModel.SStudent);
                }

                //寝室号
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.DormName == webModel.SName);
                }

                //寝室楼
                if (!string.IsNullOrEmpty(webModel.SBuilding))
                {
                    predicate = predicate.And(i => i.BuildingName == webModel.SBuilding);
                }

                var list = await bunkInfos.AsExpandable().Where(predicate).ToListAsync();
                return list.Count();
            }
        }

        #endregion

        #region Information API

        /// <summary>
        /// 根据搜索条件获取新生信息
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<IdentityUser>> GetListAsync(InformationViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SMajorClass) && string.IsNullOrEmpty(webModel.SId))
            {
                return await context.Set<IdentityUser>().AsNoTracking().Where(i => i.AccountType == 2 && i.InstructorId == CurrentUser.UserId)
                    .Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.CreatedOn).ToListAsync();
            }
            else
            {
                IQueryable<IdentityUser> students = context.IdentityUser.AsQueryable();

                var predicate = PredicateBuilder.New<IdentityUser>();

                //当前登录人数据
                predicate = predicate.And(i => i.AccountType == 2 && i.InstructorId == CurrentUser.UserId);

                //学生学号
                if (!string.IsNullOrEmpty(webModel.SId))
                {
                    predicate = predicate.And(i => i.Id == Convert.ToInt64(webModel.SId));
                }

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.Name == webModel.SName);
                }

                //专业班级名称
                if (!string.IsNullOrEmpty(webModel.SMajorClass))
                {
                    predicate = predicate.And(i => i.MajorClass.Contains(webModel.SMajorClass));
                }

                return await students.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        /// <summary>
        /// 根据搜索条件获取新生信息列表个数
        /// </summary>
        /// <param name="webModel">列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<int> GetListCountAsync(InformationViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.SMajorClass) && string.IsNullOrEmpty(webModel.SId))
            {
                var list = await context.Set<IdentityUser>().AsNoTracking().Where(i => i.AccountType == 2 && i.InstructorId == CurrentUser.UserId)
                    .Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.CreatedOn).ToListAsync();
                return list.Count();
            }
            else
            {
                IQueryable<IdentityUser> students = context.IdentityUser.AsQueryable();

                var predicate = PredicateBuilder.New<IdentityUser>();

                //当前登录人数据
                predicate = predicate.And(i => i.AccountType == 2 && i.InstructorId == CurrentUser.UserId);

                //学生学号
                if (!string.IsNullOrEmpty(webModel.SId))
                {
                    predicate = predicate.And(i => i.Id == Convert.ToInt64(webModel.SId));
                }

                //学生姓名
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.Name == webModel.SName);
                }

                //专业班级名称
                if (!string.IsNullOrEmpty(webModel.SMajorClass))
                {
                    predicate = predicate.And(i => i.MajorClass.Contains(webModel.SMajorClass));
                }

                var list = await students.AsExpandable().Where(predicate).ToListAsync();
                return list.Count();
            }
        }

        /// <summary>
        /// 获取新生信息
        /// </summary>
        /// <param name="id">学生学号</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<IdentityUser> GetEntityAsync(long id, ApplicationDbContext context)
        {
            var model = await context.IdentityUser.AsNoTracking().Where(i => i.Id == id).FirstOrDefaultAsync();
            return model;
        }

        #endregion
    }
}
