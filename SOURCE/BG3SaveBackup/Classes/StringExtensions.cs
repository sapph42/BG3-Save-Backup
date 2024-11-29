using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG3SaveBackup.Classes;

public static class StringExtensions {
	public static string? NullIfEmpty(this string? value) {
		return string.IsNullOrEmpty(value) ? null : value;
	}
	public static string? NullIfWhiteSpace(this string? value) {
		return string.IsNullOrWhiteSpace(value) ? null : value;
	}
}