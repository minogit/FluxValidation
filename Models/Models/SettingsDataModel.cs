﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ScortelApi.Models
{
    public class SettingsDataModel
    {
        /// <summary>
        /// The unique Id for this entry
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// The settings name
        /// </summary>
        /// <remarks>This column is indexed</remarks>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        /// <summary>
        /// The settings value
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }
    }
}
