using Microsoft.JSInterop;

public class WebRTCWrapper
{
    private readonly IJSRuntime _jsRuntime;

    public WebRTCWrapper(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<Peer> InitPeerAsync(string apiKey)
    {
        return new Peer(await _jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"new Peer('{apiKey}')"));
    }

    public async Task<MediaStream> GetUserMediaAsync()
    {
        var mediaStreamObjectRef = await _jsRuntime.InvokeAsync<IJSObjectReference>("navigator.mediaDevices.getUserMedia", new { audio = true, video = true });

        return new MediaStream(mediaStreamObjectRef);
    }

    public async Task<Connection> ConnectAsync(string peerId, MediaStream localStream)
    {
        var connectionObjectRef = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./connection.js");

        var connectionObject = await connectionObjectRef.InvokeAsync<IJSObjectReference>("connect", peerId, localStream.ObjectRef);

        return new Connection(connectionObject);
    }
}

public class Peer
{
    private readonly IJSObjectReference _peerObject;

    public Peer(IJSObjectReference peerObject)
    {
        _peerObject = peerObject;
    }

    public async Task<MediaConnection> CallAsync(string peerId, MediaStream localStream)
    {
        var mediaConnectionObjectRef = await _peerObject.InvokeAsync<IJSObjectReference>("call", peerId, localStream.ObjectRef);

        return new MediaConnection(mediaConnectionObjectRef);
    }
}

public class Connection
{
    private readonly IJSObjectReference _connectionObject;

    public Connection(IJSObjectReference connectionObject)
    {
        _connectionObject = connectionObject;
    }

    public async Task<MediaConnection> AnswerAsync(MediaStream localStream)
    {
        var mediaConnectionObjectRef = await _connectionObject.InvokeAsync<IJSObjectReference>("answer", localStream.ObjectRef);

        return new MediaConnection(mediaConnectionObjectRef);
    }
}

public class MediaConnection
{
    private readonly IJSObjectReference _mediaConnectionObject;

    public MediaConnection(IJSObjectReference mediaConnectionObject)
    {
        _mediaConnectionObject = mediaConnectionObject;
    }
}

public class MediaStream
{
    public IJSObjectReference ObjectRef { get; }

    public MediaStream(IJSObjectReference objectRef)
    {
        ObjectRef = objectRef;
    }
}