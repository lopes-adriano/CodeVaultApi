using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeVaultApi.Dtos.Tag
{
    public class TagDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }

}