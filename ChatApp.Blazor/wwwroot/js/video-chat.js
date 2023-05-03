let localPeerConnection = null;
let remotePeerConnection = null;

let localStream;
let remoteStream;

export function startVideoChat() {
    navigator.mediaDevices.getUserMedia({
        video: true,
        audio: false
    }).then(stream => {
            localVideo.srcObject = stream;
    })
}

export function createPeer() {
    const peer = new Peer();
    return new Promise((resolve, reject) => {
        peer.on('open', (id) => {
            resolve(id);
        });
        peer.on('error', (error) => {
            reject(error);
        });
    });
}

export async function startLocalStream() {
    localStream = await navigator.mediaDevices.getUserMedia({
        video: true,
        audio: false
    });
    return localStream;
}

export function setLocalStream(stream) {
    const localVideo = document.getElementById('localVideo');
    localVideo.srcObject = stream;
}

export function setRemoteStream(stream) {
    const remoteVideo = document.getElementById('remoteVideo');
    remoteVideo.srcObject = stream;
}

export async function callPeer(id) {
    navigator.mediaDevices.getUserMedia({
        video: true,
        audio: false
    }).then((stream) => {
        localStream = stream;
        setLocalStream(localStream);
        const call = peerInstance.call(id, localStream);
        call.on('stream', (stream) => {
            remoteStream = stream;
            setRemoteStream(remoteStream);
        })
    }).catch((error) => {
        console.log('Fail, error')
    })
}