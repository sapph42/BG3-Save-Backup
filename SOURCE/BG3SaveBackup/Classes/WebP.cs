using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BG3SaveBackup.Classes;

internal partial class WebP {
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPIDecoder { }
	public enum WEBP_CSP_MODE {
		MODE_RGB = 0,
		MODE_RGBA = 1,
		MODE_BGR = 2,
		MODE_BGRA = 3,
		MODE_ARGB = 4,
		MODE_RGBA_4444 = 5,
		MODE_RGB_565 = 6,
		MODE_rgbA = 7,
		MODE_bgrA = 8,
		MODE_Argb = 9,
		MODE_rgbA_4444 = 10,
		MODE_YUV = 11,
		MODE_YUVA = 12,
		MODE_LAST = 13
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPRGBABuffer {
		public IntPtr rgba;
		public int stride;
		public UIntPtr size;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPYUVABuffer {
		public IntPtr y;
		public IntPtr u;
		public IntPtr v;
		public IntPtr a;
		public int y_stride;
		public int u_stride;
		public int v_stride;
		public int a_stride;
		public UIntPtr y_size;
		public UIntPtr u_size;
		public UIntPtr v_size;
		public UIntPtr a_size;
	}
	[StructLayout(LayoutKind.Explicit)]
	public struct Anonymous_690ed5ec_4c3d_40c6_9bd0_0747b5a28b54 {
		[FieldOffset(0)]
		public WebPRGBABuffer RGBA;
		[FieldOffset(0)]
		public WebPYUVABuffer YUVA;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPDecBuffer {
		public WEBP_CSP_MODE colorspace;
		public int width;
		public int height;
		public int is_external_memory;
		public Anonymous_690ed5ec_4c3d_40c6_9bd0_0747b5a28b54 u;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
		public uint[] pad;
		public IntPtr private_memory;
	}
	public enum VP8StatusCode {
		VP8_STATUS_OK = 0,
		VP8_STATUS_OUT_OF_MEMORY,
		VP8_STATUS_INVALID_PARAM,
		VP8_STATUS_BITSTREAM_ERROR,
		VP8_STATUS_UNSUPPORTED_FEATURE,
		VP8_STATUS_SUSPENDED,
		VP8_STATUS_USER_ABORT,
		VP8_STATUS_NOT_ENOUGH_DATA,
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPBitstreamFeatures {
		public int width;
		public int height;
		public int has_alpha;
		public int has_animation;
		public int format;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
		public uint[] pad;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPDecoderOptions {
		public int bypass_filtering;
		public int no_fancy_upsampling;
		public int use_cropping;
		public int crop_left, crop_top;

		public int crop_width, crop_height;
		public int use_scaling;
		public int scaled_width, scaled_height;
		public int use_threads;
		public int dithering_strength;

		public int flip;
		public int alpha_dithering_strength;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
		public uint[] pad;
	};
	[StructLayout(LayoutKind.Sequential)]
	public struct WebPDecoderConfig {
		public WebPBitstreamFeatures input;
		public WebPDecBuffer output;
		public WebPDecoderOptions options;
	}

	[DllImport("libwebp", EntryPoint = "WebPGetDecoderVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern int WebPGetDecoderVersion();

	[DllImport("libwebp", EntryPoint = "WebPGetInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern int WebPGetInfo(IntPtr data, UIntPtr data_size, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPDecodeRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeRGBA(IntPtr data, UIntPtr data_size, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPDecodeARGB")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeARGB(IntPtr data, UIntPtr data_size, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPDecodeBGRA")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeBGRA(IntPtr data, UIntPtr data_size, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPDecodeRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeRGB(IntPtr data, UIntPtr data_size, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPDecodeBGR")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeBGR(IntPtr data, UIntPtr data_size, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPDecodeYUV")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeYUV(IntPtr data, UIntPtr data_size, ref int width, ref int height, ref IntPtr u, ref IntPtr v, ref int stride, ref int uv_stride);

	[DllImport("libwebp", EntryPoint = "WebPDecodeRGBAInto")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeRGBAInto(IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

	[DllImport("libwebp", EntryPoint = "WebPDecodeARGBInto")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeARGBInto(IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

	[DllImport("libwebp", EntryPoint = "WebPDecodeBGRAInto")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeBGRAInto(IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

	[DllImport("libwebp", EntryPoint = "WebPDecodeRGBInto")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeRGBInto(IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

	[DllImport("libwebp", EntryPoint = "WebPDecodeBGRInto")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeBGRInto(IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

	[DllImport("libwebp", EntryPoint = "WebPDecodeYUVInto")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPDecodeYUVInto(IntPtr data, UIntPtr data_size, IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride);

	[DllImport("libwebp", EntryPoint = "WebPInitDecBufferInternal")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern int WebPInitDecBufferInternal(ref WebPDecBuffer param0, int param1);

	[DllImport("libwebp", EntryPoint = "WebPFreeDecBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern void WebPFreeDecBuffer(ref WebPDecBuffer buffer);

	[DllImport("libwebp", EntryPoint = "WebPINewDecoder")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPINewDecoder(ref WebPDecBuffer output_buffer);

	[DllImport("libwebp", EntryPoint = "WebPINewRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPINewRGB(WEBP_CSP_MODE csp, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

	[DllImport("libwebp", EntryPoint = "WebPINewYUVA")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPINewYUVA(IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride, IntPtr a, UIntPtr a_size, int a_stride);

	[DllImport("libwebp", EntryPoint = "WebPINewYUV")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPINewYUV(IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride);

	[DllImport("libwebp", EntryPoint = "WebPIDelete")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern void WebPIDelete(ref WebPIDecoder idec);

	[DllImport("libwebp", EntryPoint = "WebPIAppend")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern VP8StatusCode WebPIAppend(ref WebPIDecoder idec, [In()] IntPtr data, UIntPtr data_size);

	[DllImport("libwebp", EntryPoint = "WebPIUpdate")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern VP8StatusCode WebPIUpdate(ref WebPIDecoder idec, [In()] IntPtr data, UIntPtr data_size);

	[DllImport("libwebp", EntryPoint = "WebPIDecGetRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPIDecGetRGB(ref WebPIDecoder idec, ref int last_y, ref int width, ref int height, ref int stride);

	[DllImport("libwebp", EntryPoint = "WebPIDecGetYUVA")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPIDecGetYUVA(ref WebPIDecoder idec, ref int last_y, ref IntPtr u, ref IntPtr v, ref IntPtr a, ref int width, ref int height, ref int stride, ref int uv_stride, ref int a_stride);

	[DllImport("libwebp", EntryPoint = "WebPIDecodedArea")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPIDecodedArea(ref WebPIDecoder idec, ref int left, ref int top, ref int width, ref int height);

	[DllImport("libwebp", EntryPoint = "WebPGetFeaturesInternal")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern VP8StatusCode WebPGetFeaturesInternal([In()] IntPtr param0, UIntPtr param1, ref WebPBitstreamFeatures param2, int param3);

	[DllImport("libwebp", EntryPoint = "WebPInitDecoderConfigInternal")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern int WebPInitDecoderConfigInternal(ref WebPDecoderConfig param0, int param1);

	[DllImport("libwebp", EntryPoint = "WebPIDecode")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern IntPtr WebPIDecode([In()] IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

	[DllImport("libwebp", EntryPoint = "WebPDecode")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
    static extern VP8StatusCode WebPDecode([In()] IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

	public const int WEBP_DECODER_ABI_VERSION = 520;
	public const int WEBP_ENCODER_ABI_VERSION = 521;
	public const int WEBP_MAX_DIMENSION = 16383;
	public static bool WebPIsPremultipliedMode(WEBP_CSP_MODE mode) {
		return (mode == WEBP_CSP_MODE.MODE_rgbA || mode == WEBP_CSP_MODE.MODE_bgrA || mode == WEBP_CSP_MODE.MODE_Argb ||
			mode == WEBP_CSP_MODE.MODE_rgbA_4444);
	}
	public static bool WebPIsRGBMode(WEBP_CSP_MODE mode) {
		return (mode < WEBP_CSP_MODE.MODE_YUV);
	}
	public static bool WebPIsAlphaMode(WEBP_CSP_MODE mode) {
		return (mode == WEBP_CSP_MODE.MODE_RGBA || mode == WEBP_CSP_MODE.MODE_BGRA || mode == WEBP_CSP_MODE.MODE_ARGB ||
				mode == WEBP_CSP_MODE.MODE_RGBA_4444 || mode == WEBP_CSP_MODE.MODE_YUVA ||
				WebPIsPremultipliedMode(mode));
	}
	public static VP8StatusCode WebPGetFeatures(IntPtr data, UIntPtr data_size, ref WebPBitstreamFeatures features) {
		return WebPGetFeaturesInternal(data, data_size, ref features, WEBP_DECODER_ABI_VERSION);
	}
	public static int WebPInitDecoderConfig(ref WebPDecoderConfig config) {
		return WebPInitDecoderConfigInternal(ref config, WEBP_DECODER_ABI_VERSION);
	}
	public static int WebPInitDecBuffer(ref WebPDecBuffer buffer) {
		return WebPInitDecBufferInternal(ref buffer, WEBP_DECODER_ABI_VERSION);
	}
	public static unsafe Bitmap DecodeFromBytes(byte[] data, long length) {
		fixed (byte* dataptr = data) {
			return DecodeFromPointer((IntPtr)dataptr, length);
		}
	}
	public static Bitmap DecodeFromPointer(IntPtr data, long length) {
		int w = 0, h = 0;
		//Validate header and determine size
		if (WebPGetInfo(data, (UIntPtr)length, ref w, ref h) == 0) throw new Exception("Invalid WebP header detected");

		bool success = false;
		Bitmap? b = null ;
		BitmapData? bd = null;
		try {
			//Allocate canvas
			b = new Bitmap(w, h, PixelFormat.Format32bppArgb);
			//Lock surface for writing
			bd = b.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			//Decode to surface
			IntPtr result = WebPDecodeBGRAInto(data, (UIntPtr)length, bd.Scan0, (UIntPtr)(bd.Stride * bd.Height), bd.Stride);
			if (bd.Scan0 != result) throw new Exception("Failed to decode WebP image with error " + (long)result);
			success = true;
		} finally {
			//Unlock surface
			if (bd != null && b != null) b.UnlockBits(bd);
			//Dispose of bitmap if anything went wrong
			if (!success && b != null) b.Dispose();
		}
		return (Bitmap)b;
	}
}
