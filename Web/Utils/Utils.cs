using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Products;
using Core.Entities.Users;
using Core.Extension;
using Web.Models;

namespace Web.Utils
{
	public static class Utils
	{
		public static List<SneakerProductViewModel> ToViewModel(this List<SneakerProduct> entities) =>
			entities.CastExtend<SneakerProduct, SneakerProductViewModel>();

		public static List<SneakerProduct> ToEntityModel(this List<SneakerProductViewModel> viewModels) =>
			viewModels.Cast<SneakerProduct>().ToList();

		public static List<UserViewModel> ToViewModel(this List<User> entities) =>
			entities.CastExtend<User, UserViewModel>();

		public static List<User> ToEntityModel(this List<UserViewModel> viewModels) => viewModels.Cast<User>().ToList();

		public static SneakerProductViewModel ToViewModel(this SneakerProduct entity) =>
			entity.CastExtend<SneakerProduct, SneakerProductViewModel>();

		public static UserViewModel ToViewModel(this User entity) => entity.CastExtend<User, UserViewModel>();
	}

}