﻿//-----------------------------------------------------------------------
// <copyright file= "SchoolRepository.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Date Created: 2018-03-11 20:59:20
// Modified by:
// Description: Administrator-School-首页功能实现仓储
//-----------------------------------------------------------------------
using LinqKit;
using Microsoft.EntityFrameworkCore;
using PSU.EFCore;
using PSU.Entity.Dormitory;
using PSU.Entity.School;
using PSU.Model.Areas.Administrator.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSU.Repository.Areas.Administrator
{
    public class SchoolRepository
    {
        #region Department API

        /// <summary>
        /// 根据搜索条件获取院系信息
        /// </summary>
        /// <param name="webModel">院系列表页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<List<Department>> GetListAsync(DepartmentViewModel webModel, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(webModel.SId) && string.IsNullOrEmpty(webModel.SName) && string.IsNullOrEmpty(webModel.STel))
            {
                return await context.Set<Department>().Skip(webModel.Start).Take(webModel.Limit).OrderByDescending(i => i.CreatedOn).ToListAsync();
            }
            else
            {
                IQueryable<Department> departments = context.Department.AsQueryable();

                var predicate = PredicateBuilder.New<Department>();

                //院系编号
                if (!string.IsNullOrEmpty(webModel.SId))
                {
                    predicate = predicate.And(i => i.Id == Convert.ToInt64(webModel.SId));
                }

                //院系名称
                if (!string.IsNullOrEmpty(webModel.SName))
                {
                    predicate = predicate.And(i => i.Name == webModel.SName);
                }

                //院系联系方式
                if (!string.IsNullOrEmpty(webModel.STel))
                {
                    predicate = predicate.And(i => i.Tel == webModel.STel);
                }

                return await departments.AsExpandable().Where(predicate).ToListAsync();
            }
        }

        /// <summary>
        /// 删除院系信息
        /// </summary>
        /// <param name="id">院系编号</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task DeleteAsync(long id, ApplicationDbContext context)
        {
            var model = await context.Department.SingleOrDefaultAsync(i => i.Id == id);

            context.Remove(model);
        }

        /// <summary>
        /// 获取院系信息
        /// </summary>
        /// <param name="id">院系编号</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<Department> GetAsync(long id, ApplicationDbContext context)
        {
            var model = await context.Department.Where(i => i.Id == id).SingleOrDefaultAsync();
            return model;
        }

        /// <summary>
        /// 新增院系信息
        /// </summary>
        /// <param name="webModel">院系编辑页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        /// <returns></returns>
        public static async Task<Department> InsertAsync(DepartmentEditViewModel webModel, ApplicationDbContext context)
        {
            var model = ConvertToDepartmentEntity(webModel);
            await context.Department.AddAsync(model);

            return model;
        }

        /// <summary>
        /// 更新院系信息
        /// </summary>
        /// <param name="webModel">院系编辑页视图模型</param>
        /// <param name="context">数据库上下文对象</param>
        public static async void UpdateAsync(DepartmentEditViewModel webModel, ApplicationDbContext context)
        {
            var model = await context.Department.SingleOrDefaultAsync(i => i.Id == Convert.ToInt64(webModel.Id));

            if (model == null)
            {
                return;
            }

            model = ConvertToDepartmentEntity(webModel, false);
        }

        #endregion

        #region MajorClass API

        #endregion

        #region Method

        /// <summary>
        /// View Model => Department Entity
        /// </summary>
        /// <param name="webModel">院系编辑页视图模型</param>
        /// <param name="isInsert">是否新增数据</param>
        /// <returns></returns>
        private static Department ConvertToDepartmentEntity(DepartmentEditViewModel webModel, bool isInsert = true)
        {
            var model = new Department
            {
                Address = webModel.Address,
                Weibo = webModel.Weibo,
                Wechat = webModel.Wechat,
                Tel = webModel.Tel,
                QQ = webModel.QQ,
                Name = webModel.Name,
                IsBranch = (int)webModel.IsBranch == 1,
                Email = webModel.Email,
                Introduction = webModel.Introduction,
                IsEnabled = (int)webModel.IsEnabled == 1
            };

            if (isInsert)
            {
                model.CreatedBy = "20171111111";
                model.CreatedName = "我是测试创建人";
            }
            else
            {
                model.ModifiedOn = DateTime.Now;
                model.ModifiedBy = "201712121212121";
                model.ModifiedName = "我是修改测试人";
            }

            return model;
        }

        #endregion
    }
}
