//using System.Threading.Tasks;
//using EasyMicroservices.Mapper.CompileTimeMapper.Interfaces;
//using EasyMicroservices.Mapper.Interfaces;
//using System.Linq;

//namespace CompileTimeMapper
//{
//    public class EmailEntity_EmailContract_Mapper : IMapper
//    {
//        readonly IMapperProvider _mapper;
//        public EmailEntity_EmailContract_Mapper(IMapperProvider mapper)
//        {
//            _mapper = mapper;
//        }
//        public global::EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity Map(global::EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract fromObject, string uniqueRecordId, string language, object[] parameters)
//        {
//            if (fromObject == default)
//                return default;
//            var mapped = new global::EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity()
//            {
//                Id = fromObject.Id,
//                ParentId = fromObject.ParentId,
//                Name = fromObject.Name,
//                Text = fromObject.Text,
//                Email = fromObject.Email,
//                Website = fromObject.Website,
//                CreationDateTime = fromObject.CreationDateTime,
//                ModificationDateTime = fromObject.ModificationDateTime,
//                IsDeleted = fromObject.IsDeleted,
//                DeletedDateTime = fromObject.DeletedDateTime,
//                UniqueIdentity = fromObject.UniqueIdentity
//            };
//            return mapped;
//        }
//        public global::EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract Map(global::EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity fromObject, string uniqueRecordId, string language, object[] parameters)
//        {
//            if (fromObject == default)
//                return default;
//            var mapped = new global::EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract()
//            {
//                Id = fromObject.Id,
//                ParentId = fromObject.ParentId,
//                Name = fromObject.Name,
//                Text = fromObject.Text,
//                Email = fromObject.Email,
//                Website = fromObject.Website,
//                CreationDateTime = fromObject.CreationDateTime,
//                ModificationDateTime = fromObject.ModificationDateTime,
//                IsDeleted = fromObject.IsDeleted,
//                DeletedDateTime = fromObject.DeletedDateTime,
//                UniqueIdentity = fromObject.UniqueIdentity
//            };
//            return mapped;
//        }
//        public async Task<global::EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity> MapAsync(global::EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract fromObject, string uniqueRecordId, string language, object[] parameters)
//        {
//            if (fromObject == default)
//                return default;
//            var mapped = new global::EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity()
//            {
//                Id = fromObject.Id,
//                ParentId = fromObject.ParentId,
//                Name = fromObject.Name,
//                Text = fromObject.Text,
//                Email = fromObject.Email,
//                Website = fromObject.Website,
//                CreationDateTime = fromObject.CreationDateTime,
//                ModificationDateTime = fromObject.ModificationDateTime,
//                IsDeleted = fromObject.IsDeleted,
//                DeletedDateTime = fromObject.DeletedDateTime,
//                UniqueIdentity = fromObject.UniqueIdentity
//            };
//            return mapped;
//        }
//        public async Task<global::EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract> MapAsync(global::EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity fromObject, string uniqueRecordId, string language, object[] parameters)
//        {
//            if (fromObject == default)
//                return default;
//            var mapped = new global::EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract()
//            {
//                Id = fromObject.Id,
//                ParentId = fromObject.ParentId,
//                Name = fromObject.Name,
//                Text = fromObject.Text,
//                Email = fromObject.Email,
//                Website = fromObject.Website,
//                CreationDateTime = fromObject.CreationDateTime,
//                ModificationDateTime = fromObject.ModificationDateTime,
//                IsDeleted = fromObject.IsDeleted,
//                DeletedDateTime = fromObject.DeletedDateTime,
//                UniqueIdentity = fromObject.UniqueIdentity

//            };
//            return mapped;
//        }
//        public object MapObject(object fromObject, string uniqueRecordId, string language, object[] parameters)
//        {
//            if (fromObject == default)
//                return default;
//            if (fromObject.GetType() == typeof(EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity))
//                return Map((EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity)fromObject, uniqueRecordId, language, parameters);
//            return Map((EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract)fromObject, uniqueRecordId, language, parameters);
//        }
//        public async Task<object> MapObjectAsync(object fromObject, string uniqueRecordId, string language, object[] parameters)
//        {
//            if (fromObject == default)
//                return default;
//            if (fromObject.GetType() == typeof(EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity))
//                return await MapAsync((EasyMicroservices.EmailsMicroservice.Database.Entities.EmailEntity)fromObject, uniqueRecordId, language, parameters);
//            return await MapAsync((EasyMicroservices.EmailsMicroservice.Contracts.Common.EmailContract)fromObject, uniqueRecordId, language, parameters);
//        }
//    }
//}