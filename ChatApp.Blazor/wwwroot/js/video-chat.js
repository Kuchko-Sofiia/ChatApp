let activeCall;
let localPeer;
let localStream;
let remoteStream;

export function setUpPeer() {
    const peer = new Peer();
    localPeer = peer;
    console.log('Peer has been created: ', peer)
    return new Promise((resolve, reject) => {
        peer.on('open', (id) => {
            resolve(id);
        });
        peer.on('call', (call) => {
            console.log('incoming call...');
            console.log('Call: ', call);
            activeCall = call;
            console.log('LocalStream: ', localStream);
            if (localStream) {
                call.answer(localStream);
                console.log('Call answered with localStream');
            }
            else {
                navigator.mediaDevices
                .getUserMedia({ video: true, audio: true })
                .then((stream) => {
                    localStream = stream;
                    call.answer(localStream);
                    console.log('Call answered with getUserMedia()');
                }).catch(function (err) {
                    console.log('Failed to get local stream', err);
                });
            }
            call.on('stream', (rStream) => {
                remoteStream = rStream;
                setRemoteVideo(getRemoteVideo(), rStream);
                setLocalVideo(getLocalVideo(), stream);
            });
            call.on('close', () => {
                console.log('Call ended');
            });
        });
        peer.on('error', (error) => {
            reject(error);
        });
    });
}

export function getLocalPeer() {
    return localPeer;
}
export function getPeerId(peer) {
    return new Promise((resolve, reject) => {
        peer.on('open', (id) => {
            resolve(id);
        });
        peer.on('call', (call) => {
            console.log('incoming call...');
            console.log('Call: ', call);
            navigator.mediaDevices
                .getUserMedia({ video: true, audio: false })
                .then((stream) => {
                    call.answer(stream);
                    console.log('Call answered');
                call.on('stream', (remoteStream) => {
                    setRemoteVideo(getRemoteVideo(), remoteStream);
                    setLocalVideo(getLocalVideo(), stream);
                });
            });
        });
        peer.on('error', (error) => {
            reject(error);
        });
    });
}

export function getLocalVideo() {
    return document.getElementById('localVideo');
}

export function getRemoteVideo() {
    return document.getElementById('remoteVideo');
}

export function setLocalVideo(localVideo, stream) {
    localVideo.srcObject = stream;
    localVideo.audio = false;
    localVideo.play();
}

export function setRemoteVideo(remoteVideo, stream) {
    remoteVideo.srcObject = stream;
    remoteVideo.play();
}

export async function getLocalStream() {
    console.log('Trying to set up local stream');
    localStream = await navigator.mediaDevices.getUserMedia({
        video: true,
        audio: true 
    });
    console.log('Local stream set up:', localStream);
    return localStream;
}


export async function callToPeer(stream, remotePeerId) {
    console.log('calling to peerId:', remotePeerId);

    // Wait for the peer connection to be open
    await new Promise((resolve) => {
        if (localPeer && localPeer.open) {
            resolve();
        } else {
            localPeer.on('open', resolve);
        }
    });

    // Make the call
    const call = localPeer.call(remotePeerId, stream);
    activeCall = call;
    call.peerConnection.onconnectionstatechange = function () {
        if (call.peerConnection.connectionState === 'connected') {
            console.log('Call connected');
        }
        console.log('Call state: ', call.peerConnection.connectionState);
    }
    console.log('Call: ', call);

    // Listen for stream from remote peer
    call.on('stream', (remoteStream) => {
        console.log(remoteStream);
        setRemoteVideo(getRemoteVideo(), remoteStream);
        setLocalVideo(getLocalVideo(), stream);
    });

    // Listen for errors
    call.on('error', (error) => {
        console.log('Call error:', error);
    });
}

export function endCall(peer) {
    // Close the PeerConnection
    if (activeCall.peerConnection) {
        activeCall.peerConnection.close();
    }
    // Close the Peer instance
    if (peer) {
        peer.destroy();
    }
    // Close the local and remote streams
    if (remoteStream) {
        remoteStream.getTracks().forEach(track => track.stop());
    }
    if (localStream) {
        localStream.getTracks().forEach(track => track.stop());
    }
}

export function closeLocalStream() {
    if (localStream) {
        localStream.getTracks().forEach(track => track.stop());
    }
}


export function subscribePeerToCalls(peer) {
    peer.on('call', (call) => {
        console.log('incoming call...');
        console.log('Call: ', call);
        navigator.mediaDevices
            .getUserMedia({ video: true, audio: true })
            .then((stream) => {
                call.answer(stream);
                console.log('Call answered');
                call.on('stream', (remoteStream) => {
                    setRemoteVideo(getRemoteVideo(), remoteStream);
                    setLocalVideo(getLocalVideo(), stream);
                });
            });
    });
    return peer;
}
//not mine
export function setLocalStream(stream) {
    const localVideo = document.getElementById('localVideo');
    localVideo.srcObject = stream;
    localVideo.audio = false;
}

//export function setRemoteStream(stream) {
//    console.log('setting remote stream', stream);
//    if (stream) {
//        const remoteVideo = document.getElementById('remoteVideo');
//        remoteVideo.srcObject = stream;
//    } else {
//        console.log('remote stream is null');
//    }
//}
