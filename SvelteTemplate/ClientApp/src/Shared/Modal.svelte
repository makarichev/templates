<script>


    export let show = false;
    let toggle = () => show = false;

    let div;

    export const modal = {
        show: x => window.$(div)
            .modal('handleUpdate')
            .modal('show'),
        hide: x => window.$(div).modal('hide')
    }
    

     
</script>



<div class="modal fade" tabindex="-1" role="dialog" bind:this={div}>
  <div class="modal-dialog {$$props.class}" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">
            <slot name="header"></slot>
        </h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
         <slot></slot>
      </div>
      <div class="modal-footer">
        <slot name="footer"></slot>      
      </div>
    </div>
  </div>
</div>