namespace Surreily.WadArchaeologist.Functionality.Model {
    public interface IWadData {
        int Length { get; }

        byte ReadByte(int position);

        byte[] ReadBytes(int position, int length);

        string ReadString(int position, int length);

        int ReadInt(int position);

        uint ReadUnsignedInt(int position);

        short ReadShort(int position);

        ushort ReadUnsignedShort(int position);
    }
}
