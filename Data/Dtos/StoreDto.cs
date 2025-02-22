﻿using System.ComponentModel.DataAnnotations;

namespace APIProdutos.Data.Dtos;

public class StoreDto
{
    public Guid StoreId { get; set; }

    [StringLength(100)]
    public string StoreName { get; set; }

    [StringLength(100)]
    public string Address { get; set; }
}
