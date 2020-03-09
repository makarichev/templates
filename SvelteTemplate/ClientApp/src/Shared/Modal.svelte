<script>
    import {Modal} from 'sveltestrap'

    export let show = false;
    let toggle = () => show = false;
     
</script>


<Modal isOpen={show} {toggle}>
    <div class="modal-header">
        <slot name="header"></slot>
        <button type="button" class="close" aria-label="Close" on:click={toggle}>
          <span aria-hidden="true">&times;</span>
        </button>    
    </div>
    <div class="modal-body">
        <slot></slot>
    </div>
    <div class="modal-footer">
        <slot name="footer"></slot>
    </div>
    
</Modal>