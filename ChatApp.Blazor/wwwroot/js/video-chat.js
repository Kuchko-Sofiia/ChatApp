let localPeerInstance;
let remoteStream = null;
let localStream;
let activeCall;

export function createNewPeer() {
    localPeerInstance = new Peer();
    console.log('Peer has been created')
    return new Promise((resolve, reject) => {
        localPeerInstance.on('open', () => {
            console.log('Peer has been created with ID:', localPeerInstance.id);
            resolve(localPeerInstance.id);
        });
        localPeerInstance.on('error', reject);
    });
}

export async function setLocalStream() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
        localStream = stream;
        setLocalVideo(getLocalVideo(), stream);
        return localStream;
    }
    catch (error) {
        console.log('Error accessing media devices.', error);
    }
    return localStream;
}

export function subscribePeerToCalls() {
    localPeerInstance.on('call', (call) => {
        console.log('incoming call...');
        console.log('Call: ', call);
        activeCall = call;
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
    console.log('Peer subscribed to calls');
}

export async function callToPeer(localStream, remotePeerId) {
    console.log('calling to peerId:', remotePeerId);

    const call = localPeerInstance.call(remotePeerId, localStream);
    activeCall = call;

    call.on('stream', (remoteStream) => {
        console.log('RemoteStream: ', remoteStream);

        setRemoteVideo(getRemoteVideo(), remoteStream);
        setLocalVideo(getLocalVideo(), localStream);
    });

    call.on('error', (error) => {
        console.log('Call error:', error);
    });
}

export function endCall() {

    if (localPeerInstance) {
        localPeerInstance.destroy();
    }

    if (remoteStream) {
        remoteStream.getTracks().forEach(track => track.stop());
    }
    console.log(localStream);
    if (localStream) {
        localStream.getTracks().forEach(track => track.stop());
    }

    localPeerInstance = null;
    localStream = null;
    activeCall = null;
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
