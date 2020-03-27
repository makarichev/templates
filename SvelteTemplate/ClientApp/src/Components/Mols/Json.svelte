<script>
    import Modal from '../../Shared/Modal.svelte'
    import {onMount} from 'svelte'
    //export let mol = null;

    let data
    let _modal;

    export const modal = {..._modal, show: async x => {
        data = JSON.stringify(x, null, 2);
        _modal.show()
    }};
    
    
</script>

<Modal bind:modal={_modal}>
    <div slot="header">JSON</div>

    {data}


</Modal>


