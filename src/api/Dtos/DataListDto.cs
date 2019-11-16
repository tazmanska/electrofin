using System;
using api.Enums;
using api.Dtos;

namespace api.Dtos
{
    public class DataListDto<TDto> where TDto: BaseDto
    {
        public TDto[] Data { get; set; }

        public int Count { get; set; }

        public int Page { get; set; }

        public int Total { get; set; }

        public int Pages { get; set; }
    }
}