using ChatApp.Blazor.Pages.Chat;
using Microsoft.JSInterop;

namespace ChatApp.Blazor.Helpers.VideoChat
{
    public class PeerJSWrapper
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference _peerJSModule;
        private IJSObjectReference _peer;

        public PeerJSWrapper(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Init(string apiKey)
        {
            _peer = await _jsRuntime.InvokeAsync<IJSObjectReference>("eval", $"new Peer('{apiKey}')");
        }

        public async Task<string> Connect(string peerId)
        {
            var conn = await _peer.InvokeAsync<IJSObjectReference>("connect", peerId);
            return await conn.InvokeAsync<string>("open");
        }

        public async Task<MediaStream> GetUserMedia()
        {
            var mediaDevices = await _jsRuntime.InvokeAsync<IJSObjectReference>("navigator.mediaDevices.getDisplayMedia");
            return await mediaDevices.InvokeAsync<MediaStream>("getUserMedia", new { video = true, audio = true });
        }

        //public async Task HandleMediaStreamEvents(string eventName, DotNetObjectReference<VideoChat> componentRef)
        //{
        //    var mediaStream = await GetUserMedia();
        //    await _jsRuntime.InvokeVoidAsync("peerjsWrapper.handleMediaStreamEvents", _peer, mediaStream, eventName, componentRef);
        //}
    }

}
