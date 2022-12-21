<template>
    <div class="input-container">
        <input
        :class="{ 'ms-input-error' : state }"
        :value="returnValue"
        :tabindex="tabindex"
        class="ms-input"
        type="date"
        @blur="getValueControl"
        @focus='this.$emit("resetError")'
        />
    </div>
</template>
<script>
export default {
    name: "MISAInputDate",
    props: ["value", "property", "tabindex", "state"],
    computed: {
        returnValue(){
            return this.convertDate(this.value);
        }
    },
    
    methods: {
        // Format ngày tháng
        convertDate(dateSrc) {
            try {
                if(dateSrc){
                    let date = new Date(dateSrc),
                        year = date.getFullYear().toString(),
                        month = (date.getMonth() + 1).toString().padStart(2, '0'),
                        day = date.getDate().toString().padStart(2, '0');
        
                    return `${year}-${month}-${day}`;
                }
            } catch (error) {
                console.log(error);
            }
        },

        // Lấy value của control
        getValueControl($event){
            try {
                let dateValue = $event.target.value;
                dateValue = new Date(dateValue);
                this.$emit("getValueControl", this.property, dateValue);
            } catch (error) {
                console.log(error);
            }
        }
    },
}
</script>
<style lang="scss">
    @import "../../css/base/input.scss";
</style>